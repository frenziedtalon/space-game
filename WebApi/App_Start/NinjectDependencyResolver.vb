
Imports System.Collections.Generic
Imports System.Diagnostics.Contracts
Imports System.Web.Http.Dependencies
Imports Ninject
Imports Ninject.Syntax

Public Class NinjectDependencyScope
    Implements IDependencyScope
    Private resolver As IResolutionRoot

    Friend Sub New(resolver As IResolutionRoot)
        Contract.Assert(resolver IsNot Nothing)

        Me.resolver = resolver
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dim disposable As IDisposable = TryCast(resolver, IDisposable)
        If disposable IsNot Nothing Then
            disposable.Dispose()
        End If

        resolver = Nothing
    End Sub

    Public Function GetService(serviceType As Type) As Object Implements IDependencyScope.GetService
        If resolver Is Nothing Then
            Throw New ObjectDisposedException("this", "This scope has already been disposed")
        End If

        Return resolver.TryGet(serviceType)
    End Function

    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyScope.GetServices
        If resolver Is Nothing Then
            Throw New ObjectDisposedException("this", "This scope has already been disposed")
        End If

        Return resolver.GetAll(serviceType)
    End Function

End Class

Public Class NinjectDependencyResolver
    Inherits NinjectDependencyScope
    Implements IDependencyResolver
    Private kernel As IKernel

    Public Sub New(kernel As IKernel)
        MyBase.New(kernel)
        Me.kernel = kernel
    End Sub

    Public Function BeginScope() As IDependencyScope Implements IDependencyResolver.BeginScope
        Return New NinjectDependencyScope(kernel.BeginBlock())
    End Function

End Class
