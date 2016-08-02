"use strict";
var runGame = () => {
    var targetObjectName = 'Mars'; //Todo: remove SG-114
    var canvas = getCanvas();
    var engine = loadBabylonEngine(canvas);
    var scene = createScene(engine);
    var scaling: Scaling;
    var navigationBarCanvas: BABYLON.ScreenSpaceCanvas2D
    endTurn();
    createCamera();
    attachUiControlEvents();
    attachWindowEvents();
    createNavigationBar();
    beginRenderLoop();

    function getCanvas(): HTMLCanvasElement {
        return <HTMLCanvasElement>document.getElementById("renderCanvas");
    }

    function loadBabylonEngine(canvas: HTMLCanvasElement): BABYLON.Engine {
        return new BABYLON.Engine(canvas, true);
    }

    function createScene(engine: BABYLON.Engine): BABYLON.Scene {
        var s = new BABYLON.Scene(engine);
        s.ambientColor = new BABYLON.Color3(1, 1, 1);
        return s;
    }

    function createSkybox(maxSize) {
        scene.clearColor = zeroColor(); // set background to black

        var skybox = BABYLON.Mesh.CreateBox("Skybox", maxSize, scene);
        var skyboxMaterial = new BABYLON.StandardMaterial("skyboxMaterial", scene);
        skyboxMaterial.backFaceCulling = false; // render the inside of the skybox
        skyboxMaterial.specularColor = zeroColor();
        skyboxMaterial.diffuseColor = zeroColor();

        // add textures to the skybox
        skyboxMaterial.reflectionTexture = new BABYLON.CubeTexture("Assets/Images/Skybox/skybox", scene);
        skyboxMaterial.reflectionTexture.coordinatesMode = BABYLON.Texture.SKYBOX_MODE;
        
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
        createSkybox(scaling.SkyBoxSize);
        //makePlanes();
        setCameraTargetFromId(turnData.Camera.CurrentTarget);
    }

    function renderSceneObjects(): void {
        if (sceneObjects !== undefined && sceneObjects !== null) {
            for (let i = 0; i < sceneObjects.length; i++) {
                sceneObjectsLookup[sceneObjects[i].Id] = sceneObjects;
                renderOrUpdateSceneObject(<BaseCelestialObject>sceneObjects[i], null);
            }
        } else {
            displayError("Scene objects undefined");
        }
    }

    function renderOrUpdateSceneObject(item: BaseCelestialObject, parent: BABYLON.Mesh): void {
        const mesh = scene.getMeshByID(item.Id);

        if (mesh === null) {
            // render the object
            sceneObjectsLookup[item.Id] = sceneObjects;
            renderSceneObject(item, parent);
        } else {
            // update it
            updateSceneObject(item, mesh as BABYLON.Mesh);
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

    function updateSceneObject(item: BaseCelestialObject, mesh: BABYLON.Mesh): void {
        if (item !== undefined && item !== null) {
            switch (item.Type) {
                case "OrbitalMechanics.CelestialObjects.Star":
                    mesh.position = createPositionFromOrbit((<OrbitingCelestialObjectBase>item).Orbit);
                    break;

                case "OrbitalMechanics.CelestialObjects.Planet":
                    mesh.position = createPositionFromOrbit((<OrbitingCelestialObjectBase>item).Orbit);
                    break;

                case "OrbitalMechanics.CelestialObjects.Moon":
                    mesh.position = createPositionFromOrbit((<OrbitingCelestialObjectBase>item).Orbit);
                    break;

                default:
                    displayError("Unknown object type: " + item.Type);
                    break;
            }
        }

        // check for satellites
        renderSatellites(item, mesh);
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

    function renderStar(info: Star): void {
        var star = renderOrbitingSphericalCelestialObject(info,
                                                            "Assets/Images/Star/" + info.Texture,
                                                            info.Radius,
                                                            null);

        // stars shine, other objects don't
        (<BABYLON.StandardMaterial>star.material).emissiveTexture = (<BABYLON.StandardMaterial>star.material).diffuseTexture;

        // create god rays effect
        var vls = new BABYLON.VolumetricLightScatteringPostProcess(info.Name + "Vls", 1.0, scene.activeCamera, star, 100, BABYLON.Texture.BILINEAR_SAMPLINGMODE, engine, false);
        vls.exposure = 0.12;

        // create a light to represent the star shining on other objects
        var starLight = new BABYLON.PointLight(info.Name + "Light", star.position, scene);
        starLight.parent = star;
    }

    function renderPlanet(info: Planet, parent: BABYLON.Mesh): void {
        renderOrbitingSphericalCelestialObject(info,
                                                "Assets/Images/Planet/" + info.Texture,
                                                info.Radius,
                                                parent);
    }

    function renderMoon(info: Moon, parent: BABYLON.Mesh): void {
        renderOrbitingSphericalCelestialObject(info,
                                                "Assets/Images/Moon/" + info.Texture,
                                                info.Radius,
                                                parent);
    }

    function renderOrbitingSphericalCelestialObject(info: OrbitingCelestialObjectBase,
                                                    texture: string,
                                                    radius: Distance,
                                                    parent: BABYLON.Mesh): BABYLON.Mesh {

        if (info.Name === targetObjectName) {
            updateCameraTarget(info.Id);
        }

        const scaledRadius = scaleRadius(radius);

        const mesh = BABYLON.Mesh.CreateSphere(info.Name, 16, scaledRadius * 2, scene);
        mesh.isPickable = info.CameraTarget;
        mesh.id = info.Id;
        mesh.position = createPositionFromOrbit(info.Orbit);
        
        const material = createDiffuseMaterial(info.Name + "Material", texture);
        material.specularColor = zeroColor();
        mesh.material = material;

        if (parent !== undefined) {
            mesh.parent = parent;
        }
        
        drawOrbit(info.Orbit, info.Name + "Orbit", parent);

        renderSatellites(info, mesh);

        return mesh;
    }

    function createDiffuseMaterial(name: string,
                            texture: string): BABYLON.StandardMaterial {
        var m = new BABYLON.StandardMaterial(name, scene);
        m.diffuseTexture = new BABYLON.Texture(texture, scene);
        m.ambientColor = new BABYLON.Color3(0.1, 0.1, 0.1);
        return m;
    }
    
    function renderSatellites(primary: BaseCelestialObject, mesh: BABYLON.Mesh): void {
        // create any satellites
        if (primary.hasOwnProperty("Satellites")) {
            for (let j = 0; j < primary.Satellites.length; j++) {
                renderOrUpdateSceneObject(primary.Satellites[j], mesh);
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
        if (!(orbit === null || orbit === undefined)) {
            const path: BABYLON.Vector3[] = new OrbitalMechanics(scaleSemiMajorAxisKilometers, orbit).generateOrbitPath();
            const colour = new BABYLON.Color3(0.54, 0.54, 0.54);
            const orbitalPath = drawPath(meshName, path, colour);

            if (parent !== undefined) {
                // positions applied are in addition to those of the parent
                orbitalPath.parent = parent;
            }
        }
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
        camera.upperRadiusLimit = 500;

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
            setNavigationBarWidth();
        });

        // listen for click events
        window.addEventListener("click", (evt: MouseEvent) => {
            // see if there's a mesh under the click
            var pickResult = scene.pick(evt.clientX, evt.clientY);
            // if there is a hit and we can select the object then set it as the camera target
            if (pickResult.hit) {
                setCameraTarget(pickResult.pickedMesh);
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

    function setCameraTargetFromId(id: string):void {
        const mesh = scene.getMeshByID(id);
        setCameraTarget(mesh);
    }

    function setCameraTarget(mesh: BABYLON.AbstractMesh): void {
        if (!(mesh === null)) {
            var limit = getMeshBoundingSphereRadius(mesh) * 1.5;

            if (limit < 1) {
                limit = 1;
            }

            (<BABYLON.ArcRotateCamera>scene.activeCamera).lowerRadiusLimit = limit;
            scene.activeCamera.parent = mesh;
        }
    }

    function setSceneScaling(bounds: SceneScaling): void {
        scaling = new Scaling(bounds);
        setCameraScaling(scaling.MaxDistance, scaling.CameraClippingDistance);
    }

    function scaleRadius(radius: Distance): number {
        return radius.Kilometers * scaling.RadiusKilometerScaleFactor;
    }

    function scaleSemiMajorAxisKilometers(semiMajorAxis: number): number {
        return semiMajorAxis * scaling.SemiMajorAxisKilometerScaleFactor;
    }
    
    function setCameraScaling(maxDistance: number, clippingDistance:number) {
        var ratio = 0.0000014410187; // based on (0.2 / max radius)
        
        var c = <BABYLON.ArcRotateCamera>scene.activeCamera;
        c.wheelPrecision = ratio * maxDistance;
        c.upperRadiusLimit = maxDistance;
        c.maxZ = clippingDistance;
        scene.activeCamera = c;
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

    function getMeshBoundingSphereRadius(mesh: BABYLON.AbstractMesh): number {
        return mesh._boundingInfo.boundingSphere.radius;
    }

    function getSceneObjectInfo(target: string): BaseGameEntity {
        if (target !== undefined && target !== null) {
            return sceneObjectsLookup[target];
        }
        return null;
    }

    function setNavigationBarWidth() {
        navigationBarCanvas.width = calculateNavigationBarWidth();
    }

    function calculateNavigationBarWidth(): number {
        const minWidth = 200;
        const padding = 200;
        var result = engine.getRenderWidth() - (2 * padding);

        return result > minWidth ? result : minWidth;
    }

    function createNavigationBar() {
        const height = 100;

        navigationBarCanvas = new BABYLON.ScreenSpaceCanvas2D(scene,
            {
                id: "NavigationBar",
                size: new BABYLON.Size(calculateNavigationBarWidth(), height),
                backgroundFill: "#C0C0C040",
                backgroundRoundRadius: 0,
                x: 200
            }
        );

    }
};

