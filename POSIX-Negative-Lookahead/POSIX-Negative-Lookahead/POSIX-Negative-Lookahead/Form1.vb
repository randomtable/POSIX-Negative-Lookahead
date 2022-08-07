Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MsgBox("This tool creates an alternative of Negative Lookahead Regex in POSIX flavor")
        MsgBox("To use this tool you need to place for each line a pattern's character")
        MsgBox("To use alternatives, you need to place in the same line needed values separated by semicolon")
        MsgBox("If you need to exclude a space, then you need to leave a space in the box on the left (see the example on the left")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RichTextBox2.Text = PNLGen(RichTextBox1.Text)
    End Sub

    Public Function PNLGen(ByVal input As String)
        Dim inputs() As String = input.Split(vbLf)
        Dim result As String = ""
        Dim part1 As String = ""
        Dim part2 As String = ""

        part1 = "/^"
        Dim acc As String = ""
        For i = 0 To inputs.Length - 1
            Dim comp() As String = inputs(i).Split(";")
            If comp.Length = 1 Then
                acc = comp(0)
            Else
                acc = "("
                For i1 = 0 To comp.Length - 1
                    If i1 = 0 Then
                        acc = acc & comp(i1)
                    Else
                        acc = acc & "|" & comp(i1)
                    End If
                Next
                acc = acc & ")"
            End If
            part1 = part1 & acc
        Next
        part1 = part1 & "[\s\S]"

        part2 = "^("
        Dim acc1 As String = ""
        Dim acc2 As String = ""
        For i = 0 To inputs.Length - 1
            If i = 0 Then
                Dim comp1() As String = inputs(i).Split(";")
                acc1 = "[^"
                For i2 = 0 To comp1.Length - 1
                    acc1 = acc1 & comp1(i2).Replace("[", "").Replace("]", "")
                Next
                acc1 = acc1 & "]+|("
                part2 = part2 & acc1
            Else
                For i1 = 0 To i - 1
                    Dim comp2() As String = inputs(i1).Split(";")
                    If comp2.Length = 1 Then
                        part2 = part2 & comp2(0)
                    Else
                        acc2 = "("
                        For i2 = 0 To comp2.Length - 1
                            If i2 = 0 Then
                                acc2 = acc2 & comp2(i2)
                            Else
                                acc2 = acc2 & "|" & comp2(i2)
                            End If
                        Next
                        acc2 = acc2 & ")"
                        part2 = part2 & acc2
                    End If
                Next
                Dim comp1() As String = inputs(i).Split(";")
                acc1 = "([^"
                For i2 = 0 To comp1.Length - 1
                    acc1 = acc1 & comp1(i2).Replace("[", "").Replace("]", "")
                Next
                If i = inputs.Length - 1 Then
                    acc1 = acc1 & "]|$)))+/"
                Else
                    acc1 = acc1 & "]|$)|"
                End If
                part2 = part2 & acc1
            End If
        Next

        result = part1 & "|" & part2
        Return result
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        My.Computer.Clipboard.SetText(RichTextBox2.Text)
        MsgBox("Copied result in clipboard")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        RichTextBox1.Text = ""
        RichTextBox2.Text = ""
    End Sub
End Class
