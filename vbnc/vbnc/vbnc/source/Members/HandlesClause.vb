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
''' HandlesClause  ::=  [  "Handles" EventHandlesList  ]
''' LAMESPEC: shouldn't it be:
''' HandlesClause  ::=  "Handles" EventHandlesList
''' </summary>
''' <remarks></remarks>
Public Class HandlesClause
    Inherits ParsedObject
    Private m_List As EventHandlesList

    ReadOnly Property List() As EventHandlesList
        Get
            Return m_list
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal List As EventHandlesList)
        m_List = List
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_List.ResolveCode(info)
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_List.ResolveTypeReferences AndAlso result

        Return result
    End Function
  
    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.Handles
    End Function
End Class
