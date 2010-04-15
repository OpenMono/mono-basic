Public Module ArrayFunctionVariableLong1
    Dim _a As Long()
    Dim _b As Long(,)
    Dim _c As Long(,,)
    Dim _d As Long(,,,)
    Dim _aa() As Long
    Dim _bb(,) As Long
    Dim _cc(,,) As Long
    Dim _dd(,,,) As Long

    Function a() As Long()
        Return _a
    End Function

    Function b() As Long(,)
        Return _b
    End Function

    Function c() As Long(,,)
        Return _c
    End Function

    Function d() As Long(,,,)
        Return _d
    End Function

    Function aa() As Long()
        Return _aa
    End Function

    Function bb() As Long(,)
        Return _bb
    End Function

    Function cc() As Long(,,)
        Return _cc
    End Function

    Function dd() As Long(,,,)
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Long() {}
        _b = New Long(,) {}
        _c = New Long(,,) {}
        _d = New Long(,,,) {}

        _aa = New Long() {}
        _bb = New Long(,) {}
        _cc = New Long(,,) {}
        _dd = New Long(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        _a = New Long() {}
        _b = New Long(,) {{}}
        _c = New Long(,,) {{{}}}
        _d = New Long(,,,) {{{{}}}}

        _aa = New Long() {}
        _bb = New Long(,) {{}}
        _cc = New Long(,,) {{{}}}
        _dd = New Long(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        _a = New Long() {1L, 2L}
        _b = New Long(,) {{10L, 11L}, {12L, 13L}}
        _c = New Long(,,) {{{20L, 21L}, {22L, 23L}}, {{24L, 25L}, {26L, 27L}}}
        _d = New Long(,,,) {{{{30L, 31L}, {32L, 33L}}, {{34L, 35L}, {36L, 37L}}}, {{{40L, 41L}, {42L, 43L}}, {{44L, 45L}, {46L, 47L}}}}

        _aa = New Long() {1L, 2L}
        _bb = New Long(,) {{10L, 11L}, {12L, 13L}}
        _cc = New Long(,,) {{{20L, 21L}, {22L, 23L}}, {{24L, 25L}, {26L, 27L}}}
        _dd = New Long(,,,) {{{{30L, 31L}, {32L, 33L}}, {{34L, 35L}, {36L, 37L}}}, {{{40L, 41L}, {42L, 43L}}, {{44L, 45L}, {46L, 47L}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        _a = New Long() {51L, 52L}
        _b = New Long(,) {{50L, 51L}, {52L, 53L}}
        _c = New Long(,,) {{{60L, 61L}, {62L, 63L}}, {{64L, 65L}, {66L, 67L}}}
        _d = New Long(,,,) {{{{70L, 71L}, {72L, 73L}}, {{74L, 75L}, {76L, 77L}}}, {{{80L, 81L}, {82L, 83L}}, {{84L, 85L}, {86L, 87L}}}}

        aa(0) = 51L
        aa(1) = 52L
        bb(0, 0) = 50L
        bb(0, 1) = 51L
        bb(1, 0) = 52L
        bb(1, 1) = 53L
        cc(0, 0, 0) = 60L
        cc(0, 0, 1) = 61L
        cc(0, 1, 0) = 62L
        cc(0, 1, 1) = 63L
        cc(1, 0, 0) = 64L
        cc(1, 0, 1) = 65L
        cc(1, 1, 0) = 66L
        cc(1, 1, 1) = 67L

        dd(0, 0, 0, 0) = 70L
        dd(0, 0, 0, 1) = 71L
        dd(0, 0, 1, 0) = 72L
        dd(0, 0, 1, 1) = 73L
        dd(0, 1, 0, 0) = 74L
        dd(0, 1, 0, 1) = 75L
        dd(0, 1, 1, 0) = 76L
        dd(0, 1, 1, 1) = 77L

        dd(1, 0, 0, 0) = 80L
        dd(1, 0, 0, 1) = 81L
        dd(1, 0, 1, 0) = 82L
        dd(1, 0, 1, 1) = 83L
        dd(1, 1, 0, 0) = 84L
        dd(1, 1, 0, 1) = 85L
        dd(1, 1, 1, 0) = 86L
        dd(1, 1, 1, 1) = 87L

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        Return result
    End Function

End Module