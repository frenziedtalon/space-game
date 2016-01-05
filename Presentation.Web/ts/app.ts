"use strict";
var createScene = () => {

    // get the canvas element from our HTML below
    var canvas = <HTMLCanvasElement>document.getElementById("renderCanvas");

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
    window.addEventListener("resize", (ev: UIEvent) => {
        engine.resize();
    });

    // listen for click events
    window.addEventListener("click", (evt: MouseEvent) => {
        // see if there's a mesh under the click
        var pickResult = scene.pick(evt.clientX, evt.clientY);
        // if there is a hit and we can select the object then set it as the camera target
        if (pickResult.hit) {
            scene.activeCamera.parent = pickResult.pickedMesh;
        }
    });

    // listen for key presses
    window.addEventListener("keypress", (evt: KeyboardEvent) => {
        if (evt.keyCode === 32) {
            // spacebar
            toggleDebugLayer();
        } else if (evt.keyCode === 97) {
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

    var sceneObjects: Array<ICelestialObject>;

    function retrieveSceneObjects() {

        $.ajax({
                url: "http://localhost/SpaceGameApi/api/Scene",
                cache: false,
                type: "GET",
                dataType: "json"
            })
            .done((data: any) => {
                // call succeeded
                renderSceneObjects(<Array<ICelestialObject>>data.Objects);
            })
            .fail((data: any) => {
                // call failed
                displayError(data);
            })
            .always((data: any) => {
                // happens after done/fail on every call
            });
    }

    function displayError(data: any) {
        // TODO: Display error details

    }

    function renderSceneObjects(objects: Array<ICelestialObject>): void {
        if (objects !== undefined && objects !== null) {
            sceneObjects = objects;
            for (var i = 0; i < objects.length; i++) {
                renderSceneObject(objects[i]);
            }
            beginRenderLoop();
        } else {
            displayError("Scene objects undefined");
        }
    }

    function renderSceneObject(item: ICelestialObject): void {
        if (item !== undefined && item !== null) {
            switch (typeof item) {
                case "Star":
                    renderStar(<Star>item);
                    break;

                case "Planet":
                    renderPlanet(<Planet>item);
                    break;

                case "Moon":
                    renderMoon(<Moon>item, null);
                    break;

                default:
                    displayError("Unknown object type: ${item.Type}");
                    break;
            }
        }
    }

    function zeroColor(): BABYLON.Color3 {
        return new BABYLON.Color3(0, 0, 0);
    }

    function createPosition(position: Point3D): BABYLON.Vector3 {
        return new BABYLON.Vector3(position.X, position.Y, position.Z);
    }

    function renderStar(starInfo: Star): void {

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

        star.isPickable = starInfo.CameraTarget;
        star.id = starInfo.Id;

        // create a light to make the star shine
        var starLight = new BABYLON.PointLight(starInfo.Name + "Light", starPosition, scene);
        starLight.intensity = 2;
        starLight.range = 380;
        starLight.parent = star;

    }

    function renderPlanet(planetInfo: Planet): void {

        var planet = BABYLON.Mesh.CreateSphere(planetInfo.Name, 16, planetInfo.Radius * 2, scene);
        planet.id = planetInfo.Id;
        planet.position = createPosition(planetInfo.Orbit.Position);

        // create a material for the planet
        var planetMaterial = new BABYLON.StandardMaterial(planetInfo.Name + "Material", scene);
        planetMaterial.diffuseTexture = new BABYLON.Texture("Assets/Images/Planet/" + planetInfo.Texture, scene);
        planetMaterial.specularColor = zeroColor();
        planet.material = planetMaterial;

        planet.isPickable = planetInfo.CameraTarget;

        // draw planet's orbit
        drawCircle(planetInfo.Orbit.Radius, planetInfo.Name + "Orbit", null);

        // create any moons
        if (planetInfo.hasOwnProperty("Moons")) {
            for (var j = 0; j < planetInfo.Moons.length; j++) {
                renderMoon(planetInfo.Moons[j], planet);
            }
        }
    }

    function renderMoon(moonInfo: Moon, parent: BABYLON.Mesh): void {
        var moon = BABYLON.Mesh.CreateSphere(moonInfo.Name, 16, moonInfo.Radius * 2, scene);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            moon.parent = parent;
        }
        moon.id = moonInfo.Id;

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

        scene.beforeRender = () => {
            if (animateScene) {
                animate();
            }
        };

        // register a render loop to repeatedly render the scene
        engine.runRenderLoop(() => {
            scene.render();
        });
    }

    function toggleAnimation() {
        animateScene = !animateScene;
    }

    // used in debugging, will not be required when turn based gameplay is implemented
    function animate(): void {
        for (var i = 0; i < sceneObjects.length; i++) {
            var target = sceneObjects[i] as OrbitingCelestialObjectBase;
            if (!(target.Orbit === undefined) && !(target.Orbit.Speed === 0)) {
                var mesh = scene.getMeshByID(target.Id);
                if (!(mesh === undefined) && !(mesh === null)) {
                    mesh.position = calculateNewOrbitPosition(target.Orbit);
                    target.Orbit.Angle += target.Orbit.Speed;
                }
            }
        }
    }

    function calculateNewOrbitPosition(orbit: Orbit): BABYLON.Vector3 {
        var x = orbit.Radius * Math.sin(orbit.Angle);
        var y = 0;
        var z = orbit.Radius * Math.cos(orbit.Angle);
        return new BABYLON.Vector3(x, y, z);
    }

    function drawCircle(radius: number, meshName: string, parent: BABYLON.Mesh) {

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

};

