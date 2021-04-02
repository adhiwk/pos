Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices

Namespace MDITest
    Module MDIClientSupport
        <DllImport("user32.dll")>
        Private Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
        End Function
        <DllImport("user32.dll")>
        Private Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
        End Function

        <DllImport("user32.dll", ExactSpelling:=True)>
        Private Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Integer
        End Function

        Private Const GWL_EXSTYLE As Integer = -20
        Private Const WS_EX_CLIENTEDGE As Integer = &H200
        Private Const SWP_NOSIZE As UInteger = &H1
        Private Const SWP_NOMOVE As UInteger = &H2
        Private Const SWP_NOZORDER As UInteger = &H4
        Private Const SWP_NOREDRAW As UInteger = &H8
        Private Const SWP_NOACTIVATE As UInteger = &H10
        Private Const SWP_FRAMECHANGED As UInteger = &H20
        Private Const SWP_SHOWWINDOW As UInteger = &H40
        Private Const SWP_HIDEWINDOW As UInteger = &H80
        Private Const SWP_NOCOPYBITS As UInteger = &H100
        Private Const SWP_NOOWNERZORDER As UInteger = &H200
        Private Const SWP_NOSENDCHANGING As UInteger = &H400

        <Extension()>
        Function SetBevel(ByVal form As Form, ByVal show As Boolean) As Boolean
            For Each c As Control In form.Controls
                Dim client As MdiClient = TryCast(c, MdiClient)

                If client IsNot Nothing Then
                    Dim windowLong As Integer = GetWindowLong(c.Handle, GWL_EXSTYLE)

                    If show Then
                        windowLong = windowLong Or WS_EX_CLIENTEDGE
                    Else
                        windowLong = windowLong And Not WS_EX_CLIENTEDGE
                    End If

                    SetWindowLong(c.Handle, GWL_EXSTYLE, windowLong)
                    SetWindowPos(client.Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOACTIVATE Or SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOZORDER Or SWP_NOOWNERZORDER Or SWP_FRAMECHANGED)
                    Return True
                End If
            Next

            Return False
        End Function
    End Module
End Namespace
