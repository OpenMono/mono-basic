' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#Const VERYEXTENDEDDEBUG = 0
#End If

''' <summary>
''' ImplementsClause  ::=  "Implements" ImplementsList
''' </summary>
''' <remarks></remarks>
Public Class MemberImplementsClause
    Inherits ParsedObject

    Private m_ImplementsList As MemberImplementsList

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ImplementsList As MemberImplementsList)
        m_ImplementsList = ImplementsList
    End Sub

    Function DefineImplements(ByVal Declaration As EventDeclaration) As Boolean
        Dim result As Boolean = True
        Dim declType As Mono.Cecil.TypeDefinition

        Helper.Assert(Declaration IsNot Nothing)

        declType = Declaration.DeclaringType.CecilType

        For i As Integer = 0 To m_ImplementsList.Count - 1
            Dim ispec As InterfaceMemberSpecifier = m_ImplementsList(i)
            Dim eventI As Mono.Cecil.EventDefinition

            eventI = CecilHelper.FindDefinition(ispec.ResolvedEventInfo)

            Helper.Assert(eventI IsNot Nothing)

            Dim addMethodI, removeMethodI, raiseMethodI As Mono.Cecil.MethodReference
            Dim addMethod, removeMethod, raiseMethod As Mono.Cecil.MethodReference

            addMethodI = Helper.GetMethodOrMethodBuilder(Compiler, eventI.AddMethod)
            removeMethodI = Helper.GetMethodOrMethodBuilder(Compiler, eventI.RemoveMethod)
            raiseMethodI = Helper.GetMethodOrMethodBuilder(Compiler, eventI.InvokeMethod)

            addMethod = Helper.GetMethodOrMethodBuilder(Compiler, Declaration.AddDefinition)
            removeMethod = Helper.GetMethodOrMethodBuilder(Compiler, Declaration.RemoveDefinition)
            raiseMethod = Helper.GetMethodOrMethodBuilder(Compiler, Declaration.RaiseDefinition)

            Helper.Assert((addMethodI Is Nothing Xor addMethod Is Nothing) = False)
            Helper.Assert((removeMethodI Is Nothing Xor removeMethod Is Nothing) = False)
            Helper.Assert((raiseMethodI Is Nothing Xor raiseMethod Is Nothing) = False)

            If addMethod IsNot Nothing AndAlso addMethodI IsNot Nothing Then
                Dim methodDef As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(addMethod)
                methodDef.Overrides.Add(Helper.GetMethodOrMethodReference(Compiler, addMethodI))
            End If
            If removeMethod IsNot Nothing AndAlso removeMethodI IsNot Nothing Then
                Dim methodDef As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(removeMethod)
                methodDef.Overrides.Add(Helper.GetMethodOrMethodReference(Compiler, removeMethodI))
            End If
            If raiseMethod IsNot Nothing AndAlso raiseMethodI IsNot Nothing Then
                Dim methodDef As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(raiseMethod)
                methodDef.Overrides.Add(Helper.GetMethodOrMethodReference(Compiler, raiseMethodI))
            End If
        Next

        Return result
    End Function

    Function DefineImplements(ByVal Method As Mono.Cecil.MethodDefinition) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Method IsNot Nothing)

        For i As Integer = 0 To m_ImplementsList.Count - 1
            Dim ispec As InterfaceMemberSpecifier = Me.m_ImplementsList(i)
            Dim methodI As Mono.Cecil.MethodReference = Nothing
            Dim propertyI As Mono.Cecil.PropertyReference = Nothing
            Dim propertyDef As Mono.Cecil.PropertyDefinition = Nothing

            If ispec.ResolvedMethodInfo IsNot Nothing Then
                methodI = Helper.GetMethodOrMethodBuilder(Compiler, ispec.ResolvedMethodInfo)
            End If

            If ispec.ResolvedPropertyInfo IsNot Nothing Then
                propertyI = Helper.GetPropertyOrPropertyBuilder(Compiler, ispec.ResolvedPropertyInfo)
                propertyDef = CecilHelper.FindDefinition(propertyI)
            End If

            Helper.Assert(propertyI Is Nothing Xor methodI Is Nothing)

            If propertyI IsNot Nothing Then
                'This is be a property
                If Method.Name.StartsWith("get_") Then
                    methodI = CecilHelper.GetCorrectMember(propertyDef.GetMethod, propertyI.DeclaringType)
                ElseIf Method.Name.StartsWith("set_") Then
                    methodI = CecilHelper.GetCorrectMember(propertyDef.SetMethod, propertyI.DeclaringType)
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
                End If
                methodI = Helper.GetMethodOrMethodBuilder(Compiler, methodI)
            End If


            Helper.Assert(methodI IsNot Nothing)

            Method.Overrides.Add(Helper.GetMethodOrMethodReference(Compiler, methodI))

#If EXTENDEDDEBUG Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Defined method override '" & Builder.FullName & ":" & Method.Name & "' overrides or implements '" & methodI.DeclaringType.FullName & ":" & methodI.Name & "'")
#End If
        Next
        Return result
    End Function

    ReadOnly Property ImplementsList() As MemberImplementsList
        Get
            Return m_ImplementsList
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_ImplementsList.ResolveCode(Info)
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_ImplementsList.ResolveTypeReferences
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.Implements
    End Function

End Class
