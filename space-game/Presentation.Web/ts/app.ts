"use strict";
var runGame = () => {
    var targetObjectName = 'Mars'; //Todo: remove SG-114

    var canvas = getCanvas();
    var engine = loadBabylonEngine(canvas);
    var scene = createScene(engine);
    var scaling: Scaling;
    var createdSkySpheres = false;

    var cameraHelper = new CameraHelper(engine);
    var meshHelper = new MeshHelper(cameraHelper);
    var mouseHelper = new MouseHelper();

    endTurn();
    createCamera();
    attachAllEvents();
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

    function createSkySpheres(): void {
        if (!createdSkySpheres) {
            createdSkySpheres = true;

            const texture = "skysphere-8192x4096.png";
            scene.clearColor = zeroColor(); // set background to black

            const innerSphere = createSkySphere(scaling.innerSkySphereDiameter, "inner", texture);
            innerSphere.material.alpha = 0.5; // make it semi-transparent so we can see the outer skysphere

            const outerSphere = createSkySphere(scaling.outerSkySphereDiameter, "outer", texture);
            outerSphere.rotation = AngleHelper.randomRotationVector();
        }
    }

    function createSkySphere(diameter: number, name: string, texture: string): BABYLON.Mesh {
        const skysphere = BABYLON.Mesh.CreateSphere("skysphere-" + name, 10, diameter, scene);

        const skysphereMaterial = new BABYLON.StandardMaterial("skysphere-" + name + "-material", scene);
        skysphereMaterial.backFaceCulling = false; // render the inside of the skybox
        skysphereMaterial.specularColor = zeroColor();
        skysphereMaterial.diffuseColor = zeroColor();
        skysphereMaterial.emissiveTexture = new BABYLON.Texture("Assets/Images/Skysphere/" + texture, scene);

        skysphere.material = skysphereMaterial;
        skysphere.isPickable = false;

        return skysphere;
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
        createSkySpheres();
        //makePlanes();
        cameraHelper.setCameraTargetFromId(turnData.Camera.CurrentTarget, scene, false);
        cameraHelper.updateNavigationCameras(scene.activeCameras, scene);
        cameraHelper.setMainSceneCameraActive(scene.activeCameras);
    }

    function renderSceneObjects(): void {
        if (sceneObjects !== undefined && sceneObjects !== null) {
            for (let i = 0; i < sceneObjects.length; i++) {
                renderOrUpdateSceneObject(<BaseCelestialObject>sceneObjects[i], null);
            }
        } else {
            displayError("Scene objects undefined");
        }
    }

    function renderOrUpdateSceneObject(item: BaseCelestialObject, parent: BABYLON.Mesh): void {
        const mesh = scene.getMeshByID(item.Id);

        if (mesh === null) {
            sceneObjectsLookup[item.Id] = item;
            renderSceneObject(item, parent);
        } else {
            updateSceneObject(item, mesh as BABYLON.Mesh, parent);
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

    function updateSceneObject(item: BaseCelestialObject, mesh: BABYLON.Mesh, parent: BABYLON.Mesh): void {
        if (item !== undefined && item !== null) {
            switch (item.Type) {
                case "OrbitalMechanics.CelestialObjects.Star":
                    mesh.position = calculateOrbitingObjectPosition((<OrbitingCelestialObjectBase>item).Orbit, parent);
                    updateOrbitPathPosition(item.Name + "Orbit", parent);
                    break;

                case "OrbitalMechanics.CelestialObjects.Planet":
                    mesh.position = calculateOrbitingObjectPosition((<OrbitingCelestialObjectBase>item).Orbit, parent);
                    updateOrbitPathPosition(item.Name + "Orbit", parent);
                    break;

                case "OrbitalMechanics.CelestialObjects.Moon":
                    mesh.position = calculateOrbitingObjectPosition((<OrbitingCelestialObjectBase>item).Orbit, parent);
                    updateOrbitPathPosition(item.Name + "Orbit", parent);
                    break;

                default:
                    displayError("Unknown object type: " + item.Type);
                    break;
            }
        }

        // check for satellites
        renderSatellites(item, mesh);
    }

    function calculateOrbitingObjectPosition(orbit: Orbit, parent: BABYLON.Mesh): BABYLON.Vector3 {
        let position = createPositionFromOrbit(orbit);
        if (parent !== undefined && parent !== null) {
            position = position.add(parent.position);
        }
        return position;
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
            info.Textures,
            info.Radius,
            null);

        BABYLON.Tags.AddTagsTo(star, "star");

        // stars shine, other objects don't
        (<BABYLON.StandardMaterial>star.material).emissiveTexture = (<BABYLON.StandardMaterial>star.material).diffuseTexture;

        // create god rays effect
        var vls = new BABYLON.VolumetricLightScatteringPostProcess(info.Name + "Vls", 1.0, scene.getCameraByName(cameraHelper.MainSceneCameraName), star, 100, BABYLON.Texture.BILINEAR_SAMPLINGMODE, engine, false);
        vls.exposure = 0.12;

        // create a light to represent the star shining on other objects
        var starLight = new BABYLON.PointLight(info.Name + "Light", star.position, scene);
        starLight.parent = star;
    }

    function renderPlanet(info: Planet, parent: BABYLON.Mesh): void {
        const planet = renderOrbitingSphericalCelestialObject(info,
            info.Textures,
            info.Radius,
            parent);

        BABYLON.Tags.AddTagsTo(planet, "planet");
        const c = new NavigationCamera(planet, scene, cameraHelper);
    }

    function renderMoon(info: Moon, parent: BABYLON.Mesh): void {
        const moon = renderOrbitingSphericalCelestialObject(info,
            info.Textures,
            info.Radius,
            parent);

        BABYLON.Tags.AddTagsTo(moon, "moon");
    }

    function renderOrbitingSphericalCelestialObject(info: OrbitingCelestialObjectBase,
        textures: Array<Texture>,
        radius: Distance,
        parent: BABYLON.Mesh): BABYLON.Mesh {

        if (info.Name === targetObjectName) {
            cameraHelper.updateCameraTarget(info.Id);
        }

        const scaledDiameter = scaleRadius(radius) * 2;

        //let scaledSma = 0;
        //if (!(info.Orbit === null || info.Orbit === undefined)) {
        //    scaledSma = scaleSemiMajorAxisKilometers(info.Orbit.SemiMajorAxis.AstronomicalUnits);
        //}

        //console.log(info.Name + " radius: " + scaledRadius + ", sma: " + scaledSma);

        const mesh = BABYLON.Mesh.CreateSphere(info.Name, 16, scaledDiameter, scene);
        mesh.isPickable = info.CameraTarget;
        mesh.id = info.Id;
        mesh.position = calculateOrbitingObjectPosition(info.Orbit, parent);

        mesh.material = createMaterial(info.Name, info.Textures);

        // render optional attributes
        createCloudLayer(info.Name, textures, scaledDiameter, mesh);
        drawOrbit(info.Orbit, info.Name + "Orbit", parent);
        renderSatellites(info, mesh);
        renderRings(info, mesh);

        // apply a small rotation. Extend this appropriately when adding object rotation about an axis SG-3, SG-4
        BABYLON.Animation.CreateAndStartAnimation(info.Name + "Rotation",
            mesh,
            "rotation.y",
            30,
            20000,
            0,
            10,
            BABYLON.Animation.ANIMATIONLOOPMODE_RELATIVE);

        return mesh;
    }

    function createMaterial(name: string, textures: Array<Texture>): BABYLON.StandardMaterial {
        const material = new BABYLON.StandardMaterial(name, scene);
        material.ambientColor = new BABYLON.Color3(0.1, 0.1, 0.1);
        material.specularColor = zeroColor();

        if (!(textures === null || textures === undefined)) {
            for (let i = 0; i < textures.length; i++) {
                addTextureFromType(material, textures[i]);
            }
        }
        return material;
    }

    function addTextureFromType(material: BABYLON.StandardMaterial, texture: Texture): void {
        const path = "Assets/Images/" + texture.Path;
        const type = TextureType[texture.Type];

        switch (type) {
            case TextureType.Bump:
                material.bumpTexture = createTexture(name + "BumpTexure", path);
                break;

            case TextureType.Diffuse:
                material.diffuseTexture = createTexture(name + "DiffuseTexure", path);
                break;

            case TextureType.Emissive:
                material.emissiveTexture = createTexture(name + "EmissiveTexure", path);
                break;

            case TextureType.Specular:
                material.specularTexture = createTexture(name + "sphereSpecularTexure", path);
                material.specularPower = 1000;
                break;

            case TextureType.Opacity:
                material.opacityTexture = createTexture(name + "OpacityTexture", path);
                material.opacityTexture.getAlphaFromRGB = true;
                break;

            default:
                break;
        }
    }

    function createTexture(name: string, texture: string): BABYLON.Texture {
        const t = new BABYLON.Texture(texture, scene);
        t.uAng = Math.PI; // Invert on vertical axis 
        t.vAng = Math.PI; // Invert on horizontal axis 
        return t;
    }

    function createCloudLayer(name: string, textures: Array<Texture>, parentDiameter: number, parent: BABYLON.Mesh): void {
        // if we have a texture for a cloud layer then create a mesh in the same position, but a little larger
        const cloudTexture = TextureHelper.getType(textures, TextureType.Clouds);

        if (cloudTexture) {
            const cloudDiameter = parentDiameter * 1.03;

            const clouds = BABYLON.Mesh.CreateSphere(name + "Clouds", 16, cloudDiameter, scene);
            const cloudMaterial = new BABYLON.StandardMaterial(name + "CloudMaterial", scene);

            // attempts to make clouds on unlit side invisible. Fix in #85.
            cloudMaterial.ambientColor = new BABYLON.Color3(0, 0, 0);
            cloudMaterial.diffuseColor = new BABYLON.Color3(1, 1, 1);
            cloudMaterial.emissiveColor = new BABYLON.Color3(0, 0, 0);
            cloudMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
            cloudMaterial.backFaceCulling = false;
            cloudMaterial.opacityTexture = createTexture(name + "CloudTransparencyTexture", "Assets/Images/" + cloudTexture.Path);
            cloudMaterial.opacityTexture.getAlphaFromRGB = true;
            clouds.material = cloudMaterial;

            clouds.isPickable = false;

            // apply a small rotation. Extend this appropriately when adding object rotation about an axis SG-3, SG-4
            BABYLON.Animation.CreateAndStartAnimation(clouds.name + "Rotation",
                clouds,
                "rotation.y",
                30,
                31000,
                0,
                10,
                BABYLON.Animation.ANIMATIONLOOPMODE_RELATIVE);
            clouds.parent = parent;

            BABYLON.Tags.AddTagsTo(clouds, "clouds");
        }
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

    function updateOrbitPathPosition(meshName: string, parent: BABYLON.Mesh) {
        const mesh = scene.getMeshByName(meshName);
        if (!(mesh === null || mesh === undefined) && !(parent === null || parent === undefined)) {
            mesh.position = parent.position;
        }
    }

    function drawOrbit(orbit: Orbit, meshName: string, parent: BABYLON.Mesh) {
        if (!(orbit === null || orbit === undefined)) {
            const path: BABYLON.Vector3[] = new OrbitalMechanics(scaleSemiMajorAxisKilometers, orbit).generateOrbitPath();
            const colour = new BABYLON.Color3(0.54, 0.54, 0.54);
            const orbitalPath = meshHelper.drawPath(meshName, path, colour, scene);

            // set layer mask so that orbit is only visible to main scene camera
            orbitalPath.layerMask = 1; // 001 in binary;
            orbitalPath.isPickable = false;

            if (parent !== undefined && parent !== null) {
                orbitalPath.position = parent.position;
            }
        }
    }

    function createCamera() {
        createArcRotateCamera();
    }

    function createArcRotateCamera() {
        var camera = new BABYLON.ArcRotateCamera(cameraHelper.MainSceneCameraName, 0, 0, 15, BABYLON.Vector3.Zero(), scene);
        camera.setPosition(new BABYLON.Vector3(0, 0, 200));
        camera.lowerRadiusLimit = 1;
        camera.upperRadiusLimit = 500;
        scene.activeCameras.push(camera);
        camera.attachControl(canvas, true);
        camera.viewport = new BABYLON.Viewport(0, 0.2, 1, 0.8);
    }

    function createVrDeviceOrientationCamera() {
        var camera = new BABYLON.VRDeviceOrientationFreeCamera("VRDOCamera", new BABYLON.Vector3(0, 50, 50), scene);
        camera.setTarget(new BABYLON.Vector3(0, 0, 0));

        // use the new camera
        scene.activeCamera = camera;

        // let the user move the camera
        camera.attachControl(canvas, false);
    }

    function attachAllEvents() {
        attachMouseEvents();
        attachKeyboardEvents();
        attachWindowEvents();
        attachUiControlEvents();
    }

    function attachWindowEvents() {
        // watch for browser/canvas resize events
        window.addEventListener("resize", (ev: UIEvent) => {
            engine.resize();
        });
    }

    function attachUiControlEvents() {
        $("#end-turn").click(() => {
            endTurn();
        });
    }

    var mouseStartPosition: Array<number> = [];

    function attachMouseEvents() {
        canvas.addEventListener("mousedown", (evt: MouseEvent) => {
            mouseStartPosition = [evt.pageX, evt.pageY];
        });

        canvas.addEventListener("mouseup", (evt: MouseEvent) => {
            var newPos = [evt.pageX, evt.pageY];
            if (mouseHelper.isClick(mouseStartPosition, newPos)) {
                meshHelper.pickMesh(evt, scene);
            }
        });
    }

    function attachKeyboardEvents() {
        // listen for key presses
        window.addEventListener("keypress", (evt: KeyboardEvent) => {
            if (evt.keyCode === 32) {
                // spacebar
                toggleDebugLayer();
            }
        });
    }

    function setSceneScaling(bounds: SceneScaling): void {
        scaling = new Scaling(bounds);
        setCameraScaling(scaling.maxDistance, scaling.cameraClippingDistance);
    }

    function scaleRadius(radius: Distance): number {
        return radius.Kilometers * scaling.RadiusKilometerScaleFactor;
    }

    function scaleSemiMajorAxisKilometers(semiMajorAxis: number): number {
        return semiMajorAxis * scaling.SemiMajorAxisKilometerScaleFactor;
    }

    function setCameraScaling(maxDistance: number, clippingDistance: number) {
        var ratio = 0.0000014410187; // based on (0.2 / max radius)

        var c = <BABYLON.ArcRotateCamera>scene.activeCamera;
        c.wheelPrecision = ratio * maxDistance;
        c.upperRadiusLimit = maxDistance;
        c.maxZ = clippingDistance;
        scene.activeCamera = c;
    }

    function makePlanes(): void {


        var leftzplane = BABYLON.Mesh.CreateGround("lzp", 50, 10, 1, scene);
        var lzpmat = new BABYLON.StandardMaterial("lzpmat", scene);
        var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        lzpmat.diffuseTexture = tex1;
        lzpmat.backFaceCulling = false;
        leftzplane.material = lzpmat;
        leftzplane.position = new BABYLON.Vector3(-30, 0, 0);
        leftzplane.rotation = new BABYLON.Vector3(0, -Math.PI / 2, 0);

        var rightzplane = BABYLON.Mesh.CreateGround("rzp", 50, 10, 1, scene);
        var rzpmat = new BABYLON.StandardMaterial("rzpmat", scene);
        // var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        rzpmat.diffuseTexture = tex1;
        rzpmat.backFaceCulling = false;
        rightzplane.material = rzpmat;
        rightzplane.position = new BABYLON.Vector3(30, 0, 0);
        rightzplane.rotation = new BABYLON.Vector3(0, -Math.PI / 2, 0);


        var frontxplane = BABYLON.Mesh.CreateGround("fxp", 70, 10, 1, scene);
        var fxpmat = new BABYLON.StandardMaterial("fxpmat", scene);
        tex1 = new BABYLON.Texture("textures/xStrip.jpg", scene);
        fxpmat.diffuseTexture = tex1;
        fxpmat.backFaceCulling = false;
        frontxplane.material = fxpmat;
        frontxplane.position = new BABYLON.Vector3(0, 0, -30);
        frontxplane.rotation = new BABYLON.Vector3(0, 0, 0);

        var rearxplane = BABYLON.Mesh.CreateGround("rxp", 70, 10, 1, scene);
        var rxpmat = new BABYLON.StandardMaterial("rxpmat", scene);
        // var tex1 = new BABYLON.Texture("textures/zStrip.jpg", scene);
        rxpmat.diffuseTexture = tex1;
        rxpmat.backFaceCulling = false;
        rearxplane.material = rxpmat;
        rearxplane.position = new BABYLON.Vector3(0, 0, 30);
        rearxplane.rotation = new BABYLON.Vector3(0, 0, 0);

        var yplane = BABYLON.Mesh.CreateGround("yp", 5, 30, 1, scene);
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

    function renderRings(info: BaseCelestialObject, parent: BABYLON.Mesh): void {
        if (info.hasOwnProperty("Rings")) {

            const scaledInnerRadius = scaleRadius(info.Rings.InnerRadius);
            const scaledOuterRadius = scaleRadius(info.Rings.OuterRadius);

            const paths = PathHelper.generatePlanetaryRingsPaths(scaledInnerRadius, scaledOuterRadius);

            const options = {
                pathArray: paths,
                closeArray: false,
                closePath: true,
                sideOrientation: BABYLON.Mesh.DOUBLESIDE,
                invertUV: true
            }

            const ribbon = BABYLON.MeshBuilder.CreateRibbon(info.Name + "Rings", options, scene);
            ribbon.isPickable = false;

            const material = createMaterial(info.Name + "Rings", info.Rings.Textures);

            if (material.opacityTexture == null) {
                material.opacityTexture = material.diffuseTexture;
                material.opacityTexture.getAlphaFromRGB = true;
            }

            ribbon.material = material;
            ribbon.parent = parent;
        }
    }
};
