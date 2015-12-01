///<reference path="scripts/babylon.max.js" />

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
    camera.lowerRadiusLimit = 50;
    camera.upperRadiusLimit = 400;

    // Use the new camera
    scene.activeCamera = camera;

    // Let the user move the camera
    camera.attachControl(canvas);

    // Create a skybox
    createSkybox();

    // Retrieve the objects to be rendered in the scene
    var sceneObjects = retrieveSceneObjects();

    if (sceneObjects != undefined) {
        
        // Render the planets
        for (i = 0; i < sceneObjects.length; i++) {
            var planet = BABYLON.Mesh.CreateSphere(sceneObjects[i].name, 16, sceneObjects[i].size, scene);
            planet.info = sceneObjects[i]
            planet.position = new BABYLON.Vector3(planet.info.orbit.radius, 0, 0);

            // Create a material for the planet
            var planetMaterial = new BABYLON.StandardMaterial(planet.info.name + 'Material', scene);
            planetMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Planet/' + planet.info.texture, scene);
            planetMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
            planet.material = planetMaterial;

            // Create any moons
            if (planet.info.hasOwnProperty('moons') && planet.info.moons.length > 0) {
                for (j = 0; j < planet.info.moons.length; j++) {
                    var moon = BABYLON.Mesh.CreateSphere(planet.info.moons[j].name, planet.info.moons[j].size, scene);
                    moon.position = new BABYLON.Vector3(planet.position.x + planet.info.moons[j].orbit.radius, planet.position.y, planet.position.z);

                    // Create a material for the moon
                    var moonMaterial = new BABYLON.StandardMaterial(planet.info.moons[j].name + 'Material', scene);
                    moonMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Moon/' + planet.info.moons[j].texture, scene);
                    moonMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
                    moon.material = moonMaterial;
                }
            }
        }

        // Animate the planets / moons before rendering the scene
        scene.beforeRender = function () {
            for (i = 0; i < sceneObjects.length; i++) {
                var planet = scene.getMeshByName(sceneObjects[i].name);
                planet.position.x = planet.info.orbit.radius * Math.sin(planet.info.orbit.angle);
                planet.position.y = 0;
                planet.position.z = planet.info.orbit.radius * Math.cos(planet.info.orbit.angle);
                planet.info.orbit.angle += planet.info.orbit.speed;

                // Create any moons
                if (planet.info.hasOwnProperty('moons') && planet.info.moons.length > 0) {
                    for (j = 0; j < planet.info.moons.length; j++) {
                        var moon = scene.getMeshByName(planet.info.moons[j].name)
                        moon.position.x = planet.position.x + (planet.info.moons[j].orbit.radius * Math.sin(planet.info.moons[j].orbit.angle));
                        moon.position.y = planet.position.y + 0;
                        moon.position.z = planet.position.z + (planet.info.moons[j].orbit.radius * Math.cos(planet.info.moons[j].orbit.angle));
                        planet.info.moons[j].orbit.angle += planet.info.moons[j].orbit.speed;
                    }
                }
            }
        };

    }

    // Register a render loop to repeatedly render the scene
    engine.runRenderLoop(function () {
        scene.render();
    });

    // Watch for browser/canvas resize events
    window.addEventListener("resize", function () {
        engine.resize();
    });

    // Listen for click events
    window.addEventListener("click", function (evt) {
        // See if there's a mesh under the click
        var pickResult = scene.pick(evt.clientX, evt.clientY);
        // If there is a hit and we can select the object then set it as the camera target
        if (pickResult.hit && (pickResult.pickedMesh.hasOwnProperty('info'))) {
            camera.target = pickResult.pickedMesh;
        };
    });

    // Listen for key presses
    window.addEventListener("keypress", function (evt) {
        if (evt.keyCode === 32) {
            toggleDebugLayer();
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

    function createPlanetList() {

        var planets = [];

        var mercury = {
            orbit: {
                radius: 40,
                speed: 0.004,
                angle: 0
            },
            name: 'Mercury',
            rotationSpeed: 0.01,
            size: 2,
            texture: 'mercury.jpg',
            selectable: true,
            moons: []
        };

        planets.push(mercury);

        var venus = {
            orbit: {
                radius: 80,
                speed: 0.0036,
                angle: 0
            },
            name: 'Venus',
            rotationSpeed: 0.01,
            size: 4,
            texture: 'venus.jpg',
            selectable: true,
            moons: []
        };

        planets.push(venus);

        var earth = {
            orbit: {
                radius: 120,
                speed: 0.0034,
                angle: 0
            },
            name: 'Earth',
            rotationSpeed: 0.01,
            size: 5,
            texture: 'earth.jpg',
            selectable: true,
            moons: []
        };

        var moon = {
            orbit: {
                radius: 8,
                speed: 0.002,
                angle: 0
            },
            name: 'Moon',
            rotationSpeed: 0.001,
            size: 1,
            texture: 'moon.jpg',
            selectable: true
        };
        earth.moons.push(moon);

        planets.push(earth);

        var mars = {
            orbit: {
                radius: 160,
                speed: 0.003,
                angle: 0
            },
            name: 'Mars',
            rotationSpeed: 0.01,
            size: 5,
            texture: 'mars.jpg',
            selectable: true,
            moons: []
        };

        planets.push(mars);

        var jupiter = {
            orbit: {
                radius: 200,
                speed: 0.0028,
                angle: 0
            },
            name: 'Jupiter',
            rotationSpeed: 0.01,
            size: 20,
            texture: 'jupiter.jpg',
            selectable: true,
            moons: []
        };

        planets.push(jupiter);

        var saturn = {
            orbit: {
                radius: 240,
                speed: 0.0025,
                angle: 0
            },
            name: 'Saturn',
            rotationSpeed: 0.01,
            size: 15,
            texture: 'saturn.jpg',
            selectable: true,
            moons: []
        };

        planets.push(saturn);

        var uranus = {
            orbit: {
                radius: 280,
                speed: 0.002,
                angle: 0
            },
            name: 'Uranus',
            rotationSpeed: 0.01,
            size: 5,
            texture: 'uranus.jpg',
            selectable: true,
            moons: []
        };

        planets.push(uranus);

        var neptune = {
            orbit: {
                radius: 320,
                speed: 0.0015,
                angle: 0
            },
            name: 'Neptune',
            rotationSpeed: 0.01,
            size: 10,
            texture: 'neptune.jpg',
            selectable: true,
            moons: []
        };

        planets.push(neptune);

        var pluto = {
            orbit: {
                radius: 360,
                speed: 0.001,
                angle: 0
            },
            name: 'Pluto',
            rotationSpeed: 0.01,
            size: 8,
            texture: 'pluto.jpg',
            selectable: true,
            moons: []
        };

        planets.push(pluto);

        return planets;
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
                debugger;
                return data;
            })
        .fail(function (data) {
            // call failed
                return [];
            })
        .always(function (data) {
            // happens after done/fail on every call
        });

    }


    }

    }

    function zeroColor() {
        return new BABYLON.Color3(0, 0, 0);
    }

    function createPosition(position) {
        // string like "x,y,z"
        var array = position.split(',')
        return new BABYLON.Vector3(array[0], array[1], array[2]);
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
        starLight.range = 380;

    }

    function renderPlanet(planetInfo) {

        var planet = BABYLON.Mesh.CreateSphere(planetInfo.Name, 16, planetInfo.Radius * 2, scene);
        planet.info = planetInfo
        planet.position = createPosition(planetInfo.Orbit.Position);

        // Create a material for the planet
        var planetMaterial = new BABYLON.StandardMaterial(planetInfo.Name + 'Material', scene);
        planetMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Planet/' + planetInfo.Texture, scene);
        planetMaterial.specularColor = zeroColor();
        planet.material = planetMaterial;

        // Create any moons
        if (planetInfo.hasOwnProperty('Moons') && planetInfo.Moons.length > 0) {
            for (j = 0; j < planetInfo.Moons.length; j++) {
                renderMoon(planetInfo.Moons[j], planet);
            }
        }
    }

    function renderMoon(moonInfo, parent) {
        var moon = BABYLON.Mesh.CreateSphere(moonInfo.Name, moonInfo.Radius * 2, scene);

        moon.position = createPosition(moonInfo.Orbit.Position);

        // Create a material for the moon
        var moonMaterial = new BABYLON.StandardMaterial(moonInfo.Name + 'Material', scene);
        moonMaterial.diffuseTexture = new BABYLON.Texture('Assets/Images/Moon/' + moonInfo.Texture, scene);
        moonMaterial.specularColor = zeroColor();
        moon.material = moonMaterial;

    }


}

