﻿"use strict";
var runGame = () => {

    var canvas = getCanvas();
    var engine = loadBabylonEngine(canvas);
    var scene = createScene(engine);
    createCamera();
    endTurn();
    attachUiControlEvents();
    attachWindowEvents();
    beginRenderLoop();

    function getCanvas(): HTMLCanvasElement {
        return <HTMLCanvasElement>document.getElementById("renderCanvas");
    }

    function loadBabylonEngine(canvas: HTMLCanvasElement): BABYLON.Engine {
        return new BABYLON.Engine(canvas, true);
    }

    function createScene(engine: BABYLON.Engine): BABYLON.Scene {
        return new BABYLON.Scene(engine);
    }

    function createSkybox() {
        scene.clearColor = zeroColor(); // set background to black

        var skybox = BABYLON.Mesh.CreateBox("Skybox", 5000, scene);
        var skyboxMaterial = new BABYLON.StandardMaterial("skyboxMaterial", scene);
        skyboxMaterial.backFaceCulling = false; // render the inside of the skybox
        skyboxMaterial.specularColor = zeroColor();
        skyboxMaterial.diffuseColor = zeroColor();

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

    var sceneObjects: Array<BaseGameEntity> = [];
    var sceneObjectsLookup: Array<string> = [];

    function endTurn() {

        $.ajax({
            url: "../SpaceGameApi/api/Turn/EndTurn",
            cache: false,
            type: "GET",
            dataType: "json"
        })
            .done((data: TurnResult) => {
                // call succeeded
                endTurnSuccess(data);
            })
            .fail((data: TurnResult) => {
                // call failed
                displayError(data);
            })
            .always((data: TurnResult) => {
                // happens after done/fail on every call
            });
    }

    function displayError(data: any) {
        // TODO: Display error details

    }

    function endTurnSuccess(turnData: TurnResult): void {
        sceneObjects = ((turnData.Scene.CelestialObjects) as Array<BaseGameEntity>);
        setSceneScaling(turnData.Scene.Scaling);
        renderSceneObjects();
        //createSkybox();
        //makePlanes();
        setCameraTarget(turnData.Camera.CurrentTarget);
    }

    function renderSceneObjects(): void {
        if (sceneObjects !== undefined && sceneObjects !== null) {
            scene.meshes = [];
            for (let i = 0; i < sceneObjects.length; i++) {
                sceneObjectsLookup[sceneObjects[i].Id] = sceneObjects;
                renderSceneObject(<BaseCelestialObject>sceneObjects[i], null);
            }
        } else {
            displayError("Scene objects undefined");
        }
    }

    function renderSceneObject(item: BaseCelestialObject, parent: BABYLON.Mesh): void {
        if (item !== undefined && item !== null) {
            switch (item.Type) {
                case "OrbitalMechanics.CelestialObjects.Star":
                    renderStar(item as Star);
                    break;

                case "OrbitalMechanics.CelestialObjects.Planet":
                    renderPlanet(item as Planet, parent);
                    break;

                case "OrbitalMechanics.CelestialObjects.Moon":
                    renderMoon(item as Moon, parent);
                    break;

                default:
                    displayError("Unknown object type: " + item.Type);
                    break;
            }
        }
    }

    function zeroColor(): BABYLON.Color3 {
        return new BABYLON.Color3(0, 0, 0);
    }

    function createPositionFromOrbit(orbit: Orbit): BABYLON.Vector3 {
        if (!(orbit === null || orbit === undefined)) {
            return createPosition(orbit.Position);
        }
        return new BABYLON.Vector3(0, 0, 0);
    }

    function createPosition(position: string): BABYLON.Vector3 {
        if (!(position === null || position === undefined)) {
            // string like "x,y,z"
            var array = position.split(",");

            var x = scaleSemiMajorAxisKilometers(parseFloat(array[1]));
            var y = scaleSemiMajorAxisKilometers(parseFloat(array[2]));
            var z = scaleSemiMajorAxisKilometers(parseFloat(array[0]));

            return new BABYLON.Vector3(x, y, z);
        }
        return new BABYLON.Vector3(0, 0, 0);
    }

    function renderStar(starInfo: Star): void {

        // create a star
        var starPosition = createPositionFromOrbit(starInfo.Orbit);

        var radius = scaleRadius(starInfo.Radius);
        var star = BABYLON.Mesh.CreateSphere(starInfo.Name, 16, radius * 2, scene);
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

        renderSatellites(starInfo, star);
    }

    function renderPlanet(planetInfo: Planet, parent: BABYLON.Mesh): void {

        var radius = scaleRadius(planetInfo.Radius);
        var planet = BABYLON.Mesh.CreateSphere(planetInfo.Name, 16, radius * 2, scene);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            planet.parent = parent;
        }

        planet.id = planetInfo.Id;
        planet.position = createPositionFromOrbit(planetInfo.Orbit);
        // create a material for the planet
        var planetMaterial = new BABYLON.StandardMaterial(planetInfo.Name + "Material", scene);
        planetMaterial.diffuseTexture = new BABYLON.Texture("Assets/Images/Planet/" + planetInfo.Texture, scene);
        planetMaterial.specularColor = zeroColor();
        planet.material = planetMaterial;

        planet.isPickable = planetInfo.CameraTarget;

        // draw planet's orbit
        drawOrbit(planetInfo.Orbit, planetInfo.Name + "Orbit", null);

        renderSatellites(planetInfo, planet);
    }

    function renderMoon(moonInfo: Moon, parent: BABYLON.Mesh): void {

        var radius = scaleRadius(moonInfo.Radius);
        var moon = BABYLON.Mesh.CreateSphere(moonInfo.Name, 16, radius * 2, scene);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            moon.parent = parent;
        }
        moon.id = moonInfo.Id;

        moon.isPickable = moonInfo.CameraTarget;

        moon.position = createPositionFromOrbit(moonInfo.Orbit);

        // create a material for the moon
        var moonMaterial = new BABYLON.StandardMaterial(moonInfo.Name + "Material", scene);
        moonMaterial.diffuseTexture = new BABYLON.Texture("Assets/Images/Moon/" + moonInfo.Texture, scene);
        moonMaterial.specularColor = zeroColor();
        moon.material = moonMaterial;

        // draw moon's orbit
        drawOrbit(moonInfo.Orbit, moonInfo.Name + "Orbit", parent);

    }

    function renderSatellites(primary: BaseCelestialObject, mesh: BABYLON.Mesh): void {
        // create any satellites
        if (primary.hasOwnProperty("Satellites")) {
            for (let j = 0; j < primary.Satellites.length; j++) {
                renderSceneObject(primary.Satellites[j], mesh);
            }
        }
    }

    function beginRenderLoop() {

        scene.beforeRender = () => {
           // nothing for now
        };

        // register a render loop to repeatedly render the scene
        engine.runRenderLoop(() => {
            scene.render();
        });
    }

    function drawOrbit(orbit: Orbit, meshName: string, parent: BABYLON.Mesh) {
        const path: BABYLON.Vector3[] = createOrbitPath(orbit.OrbitPath);
        const colour = new BABYLON.Color3(0.54, 0.54, 0.54);
        const orbitalPath = drawPath(meshName, path, colour);

        if (parent !== undefined) {
            // positions applied are in addition to those of the parent
            orbitalPath.parent = parent;
        }
    }

    function createOrbitPath(path: Array<string>): Array<BABYLON.Vector3> {
        const result: Array<BABYLON.Vector3> = [];
        for (let i = 0; i < path.length; i += 1) {
            const position: BABYLON.Vector3 = createPosition(path[i]);
            result.push(position);
        }
        return result;
    }

    function drawPath(meshName: string, path: Array<BABYLON.Vector3>, colour: BABYLON.Color3): BABYLON.LinesMesh {
        const mesh: BABYLON.LinesMesh = BABYLON.Mesh.CreateLines(meshName, path, scene);
        mesh.color = colour;
        return mesh;
    }

    function createCamera() {
        createArcRotateCamera();
    }

    function createArcRotateCamera() {
        var camera = new BABYLON.ArcRotateCamera("camera", 0, 0, 15, BABYLON.Vector3.Zero(), scene);
        camera.setPosition(new BABYLON.Vector3(0, 0, 200));
        camera.lowerRadiusLimit = 1;
        camera.upperRadiusLimit = 1500;

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

    function attachUiControlEvents() {
        $("#end-turn").click(() => {
            endTurn();
        });
    }

    function attachWindowEvents() {

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
                updateCameraTarget(pickResult.pickedMesh.id);
            }
        });

        // listen for key presses
        window.addEventListener("keypress", (evt: KeyboardEvent) => {
            if (evt.keyCode === 32) {
                // spacebar
                toggleDebugLayer();
            }
        });
    }

    function updateCameraTarget(targetId: string): void {
        const data = "target=" + targetId;

        $.ajax({
            url: "../SpaceGameApi/api/Camera/SetTarget?" + data,
            cache: false,
            type: "GET"
        })
            .done(() => {
                // call succeeded
            })
            .fail(() => {
                // call failed
            })
            .always(() => {
                // happens after done/fail on every call
            });
    }

    function setCameraTarget(target: string): void {
        const mesh = scene.getMeshByID(target);
        if (!(mesh === null)) {
            scene.activeCamera.parent = mesh;
        }
    }

    var radiusKilometerScaleFactor = 1;
    var semiMajorAxisKilometerScaleFactor = 1;

    function setSceneScaling(bounds: SceneScaling): void {

        var ratio = bounds.SemiMajorAxis.LowerBound.Kilometers / (bounds.CelestialObjectRadius.UpperBound.Kilometers * 5);

        radiusKilometerScaleFactor = (0.1 / bounds.CelestialObjectRadius.LowerBound.Kilometers);
        semiMajorAxisKilometerScaleFactor = (0.1 / bounds.SemiMajorAxis.LowerBound.Kilometers) / ratio;

        console.log("radiusKilometerScaleFactor: " + radiusKilometerScaleFactor);
        console.log("semiMajorAxisKilometerScaleFactor: " + semiMajorAxisKilometerScaleFactor);
    }

    function scaleRadius(radius: Distance): number {
        return radius.Kilometers * radiusKilometerScaleFactor;
    }

    function scaleSemiMajorAxisKilometers(semiMajorAxis: number): number {
        return semiMajorAxis * semiMajorAxisKilometerScaleFactor;
    }

    function makePlanes(): void {


        var leftzplane =  BABYLON.Mesh.CreateGround("lzp", 50, 10, 1, scene);
        var lzpmat = new BABYLON.StandardMaterial("lzpmat", scene);
        var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        lzpmat.diffuseTexture = tex1;
        lzpmat.backFaceCulling = false;
        leftzplane.material = lzpmat;
        leftzplane.position = new BABYLON.Vector3(-30, 0, 0);
        leftzplane.rotation = new BABYLON.Vector3(0, -Math.PI / 2, 0);

        var rightzplane =  BABYLON.Mesh.CreateGround("rzp", 50, 10, 1, scene);
        var rzpmat = new BABYLON.StandardMaterial("rzpmat", scene);
        // var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        rzpmat.diffuseTexture = tex1;
        rzpmat.backFaceCulling = false;
        rightzplane.material = rzpmat;
        rightzplane.position = new BABYLON.Vector3(30, 0, 0);
        rightzplane.rotation = new BABYLON.Vector3(0, -Math.PI / 2, 0);


        var frontxplane =  BABYLON.Mesh.CreateGround("fxp", 70, 10, 1, scene);
        var fxpmat = new BABYLON.StandardMaterial("fxpmat", scene);
        tex1 = new BABYLON.Texture("textures/xStrip.jpg", scene);
        fxpmat.diffuseTexture = tex1;
        fxpmat.backFaceCulling = false;
        frontxplane.material = fxpmat;
        frontxplane.position = new BABYLON.Vector3(0, 0, -30);
        frontxplane.rotation = new BABYLON.Vector3(0, 0, 0);

        var rearxplane =  BABYLON.Mesh.CreateGround("rxp", 70, 10, 1, scene);
        var rxpmat = new BABYLON.StandardMaterial("rxpmat", scene);
        // var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        rxpmat.diffuseTexture = tex1;
        rxpmat.backFaceCulling = false;
        rearxplane.material = rxpmat;
        rearxplane.position = new BABYLON.Vector3(0, 0, 30);
        rearxplane.rotation = new BABYLON.Vector3(0, 0, 0);

        var yplane =  BABYLON.Mesh.CreateGround("yp", 5, 30, 1, scene);
        var ypmat = new BABYLON.StandardMaterial("ypmat", scene);
        tex1 = new BABYLON.Texture("textures/yStrip.jpg", scene);
        ypmat.diffuseTexture = tex1;
        ypmat.backFaceCulling = false;
        yplane.material = ypmat;
        yplane.position = new BABYLON.Vector3(0, 2.3, -0.5);
        yplane.rotation = new BABYLON.Vector3(-Math.PI / 2, 0, 0);
    }
    function getSceneObjectInfo(target: string): BaseGameEntity {
        if (target !== undefined && target !== null) {
            return sceneObjectsLookup[target];
        }
        return null;
    }
};

