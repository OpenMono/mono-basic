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

''' <summary>
''' ExternalSubDeclaration ::=
''' 	[  Attributes  ]  [  ExternalMethodModifier+  ] "Declare" [  CharsetModifier  ] "Sub" Identifier
'''		LibraryClause  [  AliasClause  ]  [  (  [  ParameterList  ]  )  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ExternalSubDeclaration
    Inherits SubDeclaration

    Private m_CharsetModifier As KS
    Private m_Identifier As Identifier
    Private m_LibraryClause As LibraryClause
    Private m_AliasClause As AliasClause

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal CharsetModifier As KS, ByVal Identifier As Identifier, ByVal LibraryClause As LibraryClause, ByVal AliasClause As AliasClause, ByVal ParameterList As ParameterList)
        MyBase.Init(Attributes, Modifiers, New SubSignature(Me, Identifier.Name, ParameterList), Nothing)
        MyBase.AddModifier(KS.Shared)
        m_CharsetModifier = CharsetModifier
        m_Identifier = Identifier
        m_LibraryClause = LibraryClause
        m_AliasClause = AliasClause
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal CharsetModifier As KS, ByVal LibraryClause As LibraryClause, ByVal AliasClause As AliasClause, ByVal Signature As SubSignature)
        MyBase.Init(Attributes, Modifiers, Signature, Nothing)
        MyBase.AddModifier(KS.Shared)
        m_CharsetModifier = CharsetModifier
        m_Identifier = Identifier
        m_LibraryClause = LibraryClause
        m_AliasClause = AliasClause
    End Sub

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property

    ReadOnly Property CharsetModifier() As KS
        Get
            Return m_Charsetmodifier
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property LibraryClause() As libraryclause
        Get
            Return m_LibraryClause
        End Get
    End Property

    ReadOnly Property AliasClause() As AliasClause
        Get
            Return m_aliasclause
        End Get
    End Property

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ExternalMethodModifiers)
            i += 1
        End While
        If tm.PeekToken(i) <> KS.Declare Then Return False
        If tm.PeekToken(i + 1).Equals(ModifierMasks.CharSetModifiers) Then i += 1
        Return tm.PeekToken(i + 1) = KS.Sub AndAlso tm.PeekToken(i + 2).IsIdentifier
    End Function

    Public Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveMember(Info) AndAlso result

        Dim attrib As New Attribute(Me)
        attrib.ResolvedType = Compiler.TypeCache.System_Runtime_InteropServices_DllImportAttribute
        attrib.AttributeArguments.PositionalArgumentList.Add(m_LibraryClause.StringLiteral.StringLiteral)
        attrib.AttributeArguments.VariablePropertyInitializerList.Add("EntryPoint", Name)
        attrib.AttributeArguments.VariablePropertyInitializerList.Add("SetLastError", True)
        Select Case m_CharsetModifier
            Case KS.Auto
                attrib.AttributeArguments.VariablePropertyInitializerList.Add("CharSet", System.Runtime.InteropServices.CharSet.Auto)
            Case KS.Unicode
                attrib.AttributeArguments.VariablePropertyInitializerList.Add("CharSet", System.Runtime.InteropServices.CharSet.Unicode)
            Case KS.Ansi
                attrib.AttributeArguments.VariablePropertyInitializerList.Add("CharSet", System.Runtime.InteropServices.CharSet.Ansi)
            Case Else
                Throw New InternalException
        End Select
        Me.CustomAttributes.Add(attrib)

        Return result
    End Function


End Class
