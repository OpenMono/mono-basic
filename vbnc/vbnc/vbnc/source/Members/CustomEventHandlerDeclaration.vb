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
''' AddHandlerDeclaration  ::=
'''	[  Attributes  ]  "AddHandler" "(" ParameterList ")" LineTerminator
'''	[  Block  ]
'''	"End" "AddHandler" StatementTerminator
''' 
''' RemoveHandlerDeclaration  ::=
'''	[  Attributes  ]  "RemoveHandler" "("  ParameterList  ")"  LineTerminator
'''	[  Block  ]
'''	"End" "RemoveHandler" StatementTerminator
''' 
''' LAMESPEC: should be:
''' RemoveHandlerDeclaration  ::=
'''	[  Attributes  ]  "RemoveHandler" "("  [ ParameterList  ] ")"  LineTerminator
'''	[  Block  ]
'''	"End" "RemoveHandler" StatementTerminator
''' 
''' RaiseEventDeclaration  ::=
'''	[  Attributes  ]  "RaiseEvent" (  ParameterList  )  LineTerminator
'''	[  Block  ]
'''	"End" "RaiseEvent" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class CustomEventHandlerDeclaration
    Inherits EventHandlerDeclaration
    
    Sub New(ByVal Parent As EventDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal ParameterList As ParameterList, ByVal Block As CodeBlock, ByVal HandlerType As KS, ByVal EventName As Identifier)
        MyBase.Init(Attributes, Modifiers, HandlerType, EventName, ParameterList, Block)
        MyBase.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL Or Mono.Cecil.MethodImplAttributes.Managed
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.AddHandler, KS.RemoveHandler, KS.RaiseEvent)
    End Function

    Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveMember(Info) AndAlso result

        'Dim m_MethodAttributes As MethodAttributes
        'If m_HandlerType = KS.RaiseEvent Then
        '    m_MethodAttributes = m_MethodAttributes Or MethodAttributes.Private
        'Else
        '    m_MethodAttributes = m_MethodAttributes Or Me.Modifiers.GetMethodAttributeScope
        'End If
        'm_MethodAttributes = m_MethodAttributes Or MethodAttributes.SpecialName
        'If DeclaringType.IsInterface Then
        '    m_MethodAttributes = m_MethodAttributes Or Reflection.MethodAttributes.Abstract Or Reflection.MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride Or MethodAttributes.NewSlot
        'End If
        'If Me.IsShared Then
        '    m_MethodAttributes = m_MethodAttributes Or MethodAttributes.Static
        'End If
        'MyBase.Attributes = m_MethodAttributes

        Return result
    End Function


End Class
