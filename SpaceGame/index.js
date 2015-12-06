﻿///<reference path="scripts/babylon.max.js" />

var createScene = function () {

    // Get the canvas element from our HTML below
    var canvas = document.getElementById("renderCanvas");

    // Load the BABYLON 3D engine
    var engine = new BABYLON.Engine(canvas, true);

    // Create a scene
    var scene = new BABYLON.Scene(engine);
    scene.clearColor = new BABYLON.Color3(0, 0, 0); // Set background to black

    // Create a camera
    var camera = new BABYLON.ArcRotateCamera('camera', 0, 0, 15, BABYLON.Vector3.Zero(), scene);
    camera.setPosition(new BABYLON.Vector3(-200, 200, 0));
    camera.lowerRadiusLimit = 20;
    camera.upperRadiusLimit = 400;

    // Use the new camera
    scene.activeCamera = camera;

    // Let the user move the camera
    camera.attachControl(canvas, false);

    // Create a skybox
    createSkybox();

    // Retrieve the objects to be rendered in the scene
    retrieveSceneObjects();


    // Watch for browser/canvas resize events
    window.addEventListener("resize", function () {
        engine.resize();
    });

    // Listen for click events
    window.addEventListener("click", function (evt) {
        // See if there's a mesh under the click
        var pickResult = scene.pick(evt.clientX, evt.clientY);
        // If there is a hit and we can select the object then set it as the camera target
        if (pickResult.hit && pickResult.pickedMesh.hasOwnProperty('info') && pickResult.pickedMesh.info.CameraTarget) {
            scene.activeCamera.parent = pickResult.pickedMesh;
        }
    });

    // Listen for key presses
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
        var skybox = BABYLON.Mesh.CreateBox('Skybox', 5000, scene);
        var skyboxMaterial = new BABYLON.StandardMaterial('skyboxMaterial', scene);
        skyboxMaterial.backFaceCulling = false; // Render the inside of the skybox
        skyboxMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
        skyboxMaterial.diffuseColor = new BABYLON.Color3(0, 0, 0);

        // Add textures to the skybox
        skyboxMaterial.reflectionTexture = new BABYLON.CubeTexture('Assets/Images/Skybox/skybox', scene);
        skyboxMaterial.reflectionTexture.coordinatesMode = BABYLON.Texture.SKYBOX_MODE;

        skybox.infiniteDistance = true; // Have the skybox move with the camera so we can never move outside it
        skybox.material = skyboxMaterial;
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
            url: "api/SceneApi/",
            cache: false,
            type: 'GET',
        dataType: 'json'
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
        if (objects !== undefined) {
            for (i = 0; i < objects.length; i++) {
                renderSceneObject(objects[i]);
            }
            createShadowGenerators();
            beginRenderLoop();
        } else {
            displayError('Scene objects undefined');
        }
    }

    function renderSceneObject(item) {
        if (item !== undefined) {
            switch (item.Type) {
                case 'OrbitalMechanics.CelestialObjects.Star':
                    renderStar(item);
                    break;
                
                case 'OrbitalMechanics.CelestialObjects.Planet':
                    renderPlanet(item);
                    break;

                case 'OrbitalMechanics.CelestialObjects.Moon':
                    renderMoon(item);
                    break;

                default:
                    displayError('Unknown object type: ' + item.Type)
                    break;
            }
        }
    }

    function zeroColor() {
        return new BABYLON.Color3(0, 0, 0);
    }

    function createPosition(position) {
        // string like "x,y,z"
        var array = position.split(',')
        return new BABYLON.Vector3(parseInt(array[0]), parseInt(array[1]), parseInt(array[2]));
    }

    function renderStar(starInfo) {
        
        // Create a star
        var starPosition = createPosition(starInfo.Orbit.Position);
        
        var star = BABYLON.Mesh.CreateSphere(starInfo.Name, 16, starInfo.Radius * 2, scene);
        star.position = starPosition;

        // Create the material for the star, removing its reaction to other light sources
        var starMaterial = new BABYLON.StandardMaterial(starInfo.Name + 'Material', scene);
        starMaterial.emissiveTexture = new BABYLON.Texture('Assets/Images/Star/' + starInfo.Texture, scene);
        starMaterial.specularColor = zeroColor();
        starMaterial.diffuseColor = zeroColor();

        star.material = starMaterial;

        star.info = starInfo;
      
        // Create a light to make the star shine
        var starLight = new BABYLON.PointLight(starInfo.Name + 'Light', starPosition, scene);
        starLight.intensity = 2;
        starLight.range = 400;

        lightSources.push(starLight);

    }

    function renderPlanet(planetInfo) {

        var planet = BABYLON.Mesh.CreateSphere(planetInfo.Name, 16, planetInfo.Radius * 2, scene);
        planet.info = planetInfo;
        planet.position = createPosition(planetInfo.Orbit.Position);

        // Create a material for the planet
        var planetMaterial = new BABYLON.StandardMaterial(planetInfo.Name + 'Material', scene);
        planetMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Planet/' + planetInfo.Texture, scene);
        planetMaterial.specularColor = zeroColor();
        planet.material = planetMaterial;

        // Create any moons
        if (planetInfo.hasOwnProperty('Moons')) {
            for (j = 0; j < planetInfo.Moons.length; j++) {
                renderMoon(planetInfo.Moons[j], planet);
            }
        }
    }

    function renderMoon(moonInfo, parent) {
        var moon = BABYLON.Mesh.CreateSphere(moonInfo.Name, 16, moonInfo.Radius * 2, scene);

        if (parent !== undefined) {
            // Positions applied are in addition to those of the parent
            moon.parent = parent;
        }
        moon.info = moonInfo;

        moon.position = createPosition(moonInfo.Orbit.Position);

        // Create a material for the moon
        var moonMaterial = new BABYLON.StandardMaterial(moonInfo.Name + 'Material', scene);
        moonMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Moon/' + moonInfo.Texture, scene);
        moonMaterial.specularColor = zeroColor();
        moon.material = moonMaterial;

    }

    function beginRenderLoop() {

        scene.beforeRender = function () {
            if (animateScene) {
                animate();
            }
        }

        // Register a render loop to repeatedly render the scene
        engine.runRenderLoop(function () {
            scene.render();
        });
    }

    var animateScene = false;

    function toggleAnimation() {
        animateScene = !animateScene;
    }

    // Used in debugging, will not be required when turn based gameplay is implemented
    function animate() {
        var meshes = scene.meshes;
        for (i = 0; i < meshes.length; i++) {
            var mesh = meshes[i];
            if (mesh.hasOwnProperty('info') && !(mesh.info.Orbit === undefined)
                && !(mesh.info.Orbit.Speed === 0)) {
                mesh.position.x = mesh.info.Orbit.Radius * Math.sin(mesh.info.Orbit.Angle);
                mesh.position.y = 0;
                mesh.position.z = mesh.info.Orbit.Radius * Math.cos(mesh.info.Orbit.Angle);
                mesh.info.Orbit.Angle += mesh.info.Orbit.Speed;
            }
        }
    }

    lightSources = [];

    function createShadowGenerators(light) {
        if (lightSources.length > 0) {
            var meshes = scene.meshes;
            for (i = 0; i < lightSources.length; i++) {
                var light = lightSources[i];

                // Create a shadow generator for each light source
                var shadowGenerator = new BABYLON.ShadowGenerator(1024, light);
                //shadowGenerator.useVarianceShadowMap = true;

                // Each mesh will be able to both cast a shadow and be in shadow
                for (j = 0; j < meshes.length; j++) {
                    var mesh = meshes[i];
                    shadowGenerator.getShadowMap().renderList.push(mesh);
                    mesh.receiveShadows = true;
                }
            }
        }
        

        

    }

}

