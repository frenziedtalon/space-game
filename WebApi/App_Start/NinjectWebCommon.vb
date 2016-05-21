Imports Camera
Imports Data
Imports Data.InMemoryData
Imports Entities
Imports Microsoft.Web.Infrastructure.DynamicModuleHelper
Imports Ninject
Imports Ninject.Web.Common
Imports TurnTracker
Imports WebApi.Services

<Assembly: WebActivatorEx.PreApplicationStartMethod(GetType(WebApi.App_Start.NinjectWebCommon), "Start")>
<Assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(GetType(WebApi.App_Start.NinjectWebCommon), "Stop")>
Namespace WebApi.App_Start



    Public NotInheritable Class NinjectWebCommon
        Private Sub New()
        End Sub
        Private Shared ReadOnly bootstrapper As New Bootstrapper()

        ''' <summary>
        ''' Starts the application
        ''' </summary>
        Public Shared Sub Start()
            DynamicModuleUtility.RegisterModule(GetType(OnePerRequestHttpModule))
            DynamicModuleUtility.RegisterModule(GetType(NinjectHttpModule))
            bootstrapper.Initialize(AddressOf CreateKernel)
        End Sub

        ''' <summary>
        ''' Stops the application.
        ''' </summary>
        Public Shared Sub [Stop]()
            bootstrapper.ShutDown()
        End Sub

        ''' <summary>
        ''' Creates the kernel that will manage your application.
        ''' </summary>
        ''' <returns>The created kernel.</returns>
        Private Shared Function CreateKernel() As IKernel
            Dim kernel = New StandardKernel()
            Try
                kernel.Bind(Of Func(Of IKernel))().ToMethod(Function(ctx) Function() New Bootstrapper().Kernel)
                kernel.Bind(Of IHttpModule)().[To](Of HttpApplicationInitializationHttpModule)()

                RegisterServices(kernel)
                Return kernel
            Catch
                kernel.Dispose()
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Load your modules or register your services here!
        ''' </summary>
        ''' <param name="kernel">The kernel.</param>
        Private Shared Sub RegisterServices(kernel As IKernel)

            kernel.Bind(Of IEntityManager)().To(Of EntityManager)().InSingletonScope()
            kernel.Bind(Of ITurnTracker)().To(Of InMemoryTurnTracker.TurnTracker)().InSingletonScope()
            kernel.Bind(Of ISceneService)().To(Of SceneService)()
            kernel.Bind(Of ICameraService)().To(Of InMemoryCamera.CameraService)().InSingletonScope()
            kernel.Bind(Of IDataProvider)().To(Of DataProvider)().InSingletonScope()
            kernel.Bind(Of ISolarSystemData)().To(Of SolarSystemData)().InSingletonScope()
        End Sub
    End Class
End Namespace
