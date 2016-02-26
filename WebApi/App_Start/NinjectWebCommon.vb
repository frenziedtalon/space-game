Imports System.Web.Http
Imports Entities
Imports Microsoft.Web.Infrastructure.DynamicModuleHelper
Imports Ninject
Imports Ninject.Web.Common

<Assembly: WebActivatorEx.PreApplicationStartMethod(GetType(WebApi.App_Start.NinjectMVC3), "StartNinject")>
<Assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(GetType(WebApi.App_Start.NinjectMVC3), "StopNinject")>

Namespace WebApi.App_Start
    Public Module NinjectMVC3
        Private ReadOnly bootstrapper As New Bootstrapper()

        ''' <summary>
        ''' Starts the application
        ''' </summary>
        Public Sub StartNinject()
            DynamicModuleUtility.RegisterModule(GetType(OnePerRequestHttpModule))
            bootstrapper.Initialize(AddressOf CreateKernel)
        End Sub

        ''' <summary>
        ''' Stops the application.
        ''' </summary>
        Public Sub StopNinject()
            bootstrapper.ShutDown()
        End Sub

        ''' <summary>
        ''' Creates the kernel that will manage your application.
        ''' </summary>
        ''' <returns>The created kernel.</returns>
        Private Function CreateKernel() As IKernel
            Dim kernel = New StandardKernel()

            kernel.Bind(Of Func(Of IKernel))().ToMethod(Function(ctx) Function() New Bootstrapper().Kernel)
            kernel.Bind(Of IHttpModule)().[To](Of HttpApplicationInitializationHttpModule)()



            RegisterServices(kernel)

            ' Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = New NinjectDependencyResolver(kernel)
            Return kernel
        End Function

        ''' <summary>
        ''' Load your modules or register your services here!
        ''' </summary>
        ''' <param name="kernel">The kernel.</param>
        Private Sub RegisterServices(ByVal kernel As IKernel)
            kernel.Bind(Of IEntityManager).[To](Of EntityManager)()
        End Sub
    End Module
End Namespace