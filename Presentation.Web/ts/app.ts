/// <reference path="references.ts" />
"use strict";

var createScene = () => {

    var canvas = document.getElementById("renderCanvas");
    // get the canvas element from our HTML below

    // load the BABYLON 3D engine
    var engine = new BABYLON.Engine(canvas, true);

    // create a scene
    var scene = new BABYLON.Scene(engine);
    scene.clearColor = new BABYLON.Color3(0, 0, 0); // set background to black

    // create a camera
    createCamera();

    // create a skybox
    createSkybox();

    // retrieve the objects to be rendered in the scene
    retrieveSceneObjects();

    // watch for browser/canvas resize events
    window.addEventListener("resize", function () {
        engine.resize();
    });

    // listen for click events
    window.addEventListener("click", function (evt) {
        // see if there's a mesh under the click
        var pickResult = scene.pick(evt.clientX, evt.clientY);
        // if there is a hit and we can select the object then set it as the camera target
        if (pickResult.hit) {
            scene.activeCamera.parent = pickResult.pickedMesh;
        }
    });

    // listen for key presses
    window.addEventListener("keypress", function (evt) {
        if (evt.keyCode === 32) {
            // spacebar
            toggleDebugLayer();
        }
        else if (evt.keyCode === 97) {
            // a
            toggleAnimation();
        }
    });

    function createSkybox() {
        var skybox = BABYLON.Mesh.CreateBox("Skybox", 5000, scene);
        var skyboxMaterial = new BABYLON.StandardMaterial("skyboxMaterial", scene);
        skyboxMaterial.backFaceCulling = false; // render the inside of the skybox
        skyboxMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
        skyboxMaterial.diffuseColor = new BABYLON.Color3(0, 0, 0);

        // add textures to the skybox
        skyboxMaterial.reflectionTexture = new BABYLON.CubeTexture("Assets/Images/Skybox/skybox", scene);
        skyboxMaterial.reflectionTexture.coordinatesMode = BABYLON.Texture.SKYBOX_MODE;

        skybox.infiniteDistance = true; // have the skybox move with the camera so we can never move outside it
        skybox.material = skyboxMaterial;
        skybox.isPickable = false;
    }

    function toggleDebugLayer() {
        if (scene.debugLayer.isVisible()) {
            scene.debugLayer.hide();
        } else {
            scene.debugLayer.show();
        }
    }

    function retrieveSceneObjects() {

        $.ajax({
                url: "http://localhost/SpaceGameApi/api/Scene",
                cache: false,
                type: "GET",
                dataType: "json"
            })
            .done(function (data) {
                // call succeeded
                renderSceneObjects(data.Objects);
            })
            .fail(function (data) {
                // call failed
                displayError(data);
            })
            .always(function (data) {
                // happens after done/fail on every call
            });

    }

    function displayError(data) {
        //TODO: Display error details

    }

    function renderSceneObjects(objects) {
        if (objects !== undefined && objects !== null) {
            for (var i = 0; i < objects.length; i++) {
                renderSceneObject(objects[i]);
            }
            beginRenderLoop();
        } else {
            displayError("Scene objects undefined");
        }
    }

    function renderSceneObject(item) {
        if (item !== undefined && item !== null) {
            switch (item.Type) {
            case "OrbitalMechanics.CelestialObjects.Star":
                renderStar(item);
                break;

            case "OrbitalMechanics.CelestialObjects.Planet":
                renderPlanet(item);
                break;

            case "OrbitalMechanics.CelestialObjects.Moon":
                renderMoon(item);
                break;

            default:
                displayError("Unknown object type: " + item.Type);
                break;
            }
        }
    }

    function zeroColor() {
        return new BABYLON.Color3(0, 0, 0);
    }

    function createPosition(position) {
        // string like "x,y,z"
        var array = position.split(",");
        return new BABYLON.Vector3(parseInt(array[0], 10), parseInt(array[1], 10), parseInt(array[2], 10));
    }

    function renderStar(starInfo) {

        // create a star
        var starPosition = createPosition(starInfo.Orbit.Position);

        var star = BABYLON.Mesh.CreateSphere(starInfo.Name, 16, starInfo.Radius * 2, scene);
        star.position = starPosition;

        // create the material for the star, removing its reaction to other light sources
        var starMaterial = new BABYLON.StandardMaterial(starInfo.Name + "Material", scene);
        starMaterial.emissiveTexture = new BABYLON.Texture("Assets/Images/Star/" + starInfo.Texture, scene);
        starMaterial.specularColor = zeroColor();
        starMaterial.diffuseColor = zeroColor();

        star.material = starMaterial;

        star.info = starInfo;
        star.isPickable = starInfo.CameraTarget;

        // create a light to make the star shine
        var starLight = new BABYLON.PointLight(starInfo.Name + "Light", starPosition, scene);
        starLight.intensity = 2;
        starLight.range = 380;
        starLight.parent = star;

    }

    function renderPlanet(planetInfo) {

        var planet = BABYLON.Mesh.CreateSphere(planetInfo.Name, 16, planetInfo.Radius * 2, scene);
        planet.info = planetInfo;
        planet.position = createPosition(planetInfo.Orbit.Position);

        // create a material for the planet
        var planetMaterial = new BABYLON.StandardMaterial(planetInfo.Name + "Material", scene);
        planetMaterial.diffuseTexture = new BABYLON.Texture("Assets/Images/Planet/" + planetInfo.Texture, scene);
        planetMaterial.specularColor = zeroColor();
        planet.material = planetMaterial;

        planet.isPickable = planetInfo.CameraTarget;

        // draw planet's orbit
        drawCircle(planetInfo.Orbit.Radius, planetInfo.Name + "Orbit");

        // create any moons
        if (planetInfo.hasOwnProperty("Moons")) {
            for (var j = 0; j < planetInfo.Moons.length; j++) {
                renderMoon(planetInfo.Moons[j], planet);
            }
        }
    }

    function renderMoon(moonInfo, parent) {
        var moon = BABYLON.Mesh.CreateSphere(moonInfo.Name, 16, moonInfo.Radius * 2, scene);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            moon.parent = parent;
        }
        moon.info = moonInfo;

        moon.isPickable = moonInfo.CameraTarget;

        moon.position = createPosition(moonInfo.Orbit.Position);

        // create a material for the moon
        var moonMaterial = new BABYLON.StandardMaterial(moonInfo.Name + "Material", scene);
        moonMaterial.diffuseTexture = new BABYLON.Texture("Assets/Images/Moon/" + moonInfo.Texture, scene);
        moonMaterial.specularColor = zeroColor();
        moon.material = moonMaterial;

        // draw moon's orbit
        drawCircle(moonInfo.Orbit.Radius, moonInfo.Name + "Orbit", parent);

    }

    var animateScene = true;

    function beginRenderLoop() {

        scene.beforeRender = function () {
            if (animateScene) {
                animate();
            }
        }

        // register a render loop to repeatedly render the scene
        engine.runRenderLoop(function () {
            scene.render();
        });
    }

    function toggleAnimation() {
        animateScene = !animateScene;
    }

    // used in debugging, will not be required when turn based gameplay is implemented
    function animate() {
        var meshes = scene.meshes;
        for (var i = 0; i < meshes.length; i++) {
            var mesh = meshes[i];
            if (mesh.hasOwnProperty("info") && !(mesh.info.Orbit === undefined)
                && !(mesh.info.Orbit.Speed === 0)) {
                mesh.position.x = mesh.info.Orbit.Radius * Math.sin(mesh.info.Orbit.Angle);
                mesh.position.y = 0;
                mesh.position.z = mesh.info.Orbit.Radius * Math.cos(mesh.info.Orbit.Angle);
                mesh.info.Orbit.Angle += mesh.info.Orbit.Speed;

            }
        }
    }

    function drawCircle(radius, meshName, parent) {

        var tes = radius / 2; // number of path points, more is smoother
        if (tes < 40) {
            tes = 40;
        } else if (tes > 200) {
            tes = 200;
        }
        var pi2 = Math.PI * 2;
        var step = pi2 / tes;
        var path = [];
        for (var i = 0; i < pi2; i += step) {
            var x = radius * Math.sin(i);
            var y = 0;
            var z = radius * Math.cos(i);
            path.push(new BABYLON.Vector3(x, y, z));
        }
        path.push(path[0]);

        var circle = BABYLON.Mesh.CreateLines(meshName, path, scene);
        circle.color = new BABYLON.Color3(0.54, 0.54, 0.54);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            circle.parent = parent;
        }

    }

    function createCamera() {
        createArcRotateCamera();
    }

    function createArcRotateCamera() {
        var camera = new BABYLON.ArcRotateCamera("camera", 0, 0, 15, BABYLON.Vector3.Zero(), scene);
        camera.setPosition(new BABYLON.Vector3(-200, 200, 0));
        camera.lowerRadiusLimit = 50;
        camera.upperRadiusLimit = 400;

        // use the new camera
        scene.activeCamera = camera;

        // let the user move the camera
        camera.attachControl(canvas, false);
    }

    function createVrDeviceOrientationCamera() {
        var camera = new BABYLON.VRDeviceOrientationFreeCamera("VRDOCamera", new BABYLON.Vector3(0, 50, 50), scene);
        camera.setTarget(new BABYLON.Vector3(0, 0, 0));

        // use the new camera
        scene.activeCamera = camera;

        // let the user move the camera
        camera.attachControl(canvas, false);

    }

}

