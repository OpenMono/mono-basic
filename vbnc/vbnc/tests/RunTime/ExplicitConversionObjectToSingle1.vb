Module ExplicitConversionObjectToSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Single
        Dim const2 As Single

        value1 = Nothing
        value2 = CSng(value1)
        const2 = CSng(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToSingle1")
            Return 1
        End If
    End Function
End Module
