Imports System.Data.SqlClient
Module PublicModule
    Public Function RolesBasedAccessControl(ByVal cUserId As String,
                                             ByVal cParent As String,
                                             ByVal cProcess As String) As Boolean
        Dim lRoles As Boolean = False

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL()
        Conn.Open()

        Try
            Using Cmd As SqlCommand = New SqlCommand() With {.Connection = Conn}
                Cmd.CommandText = "SELECT access_control_rule.[access] " &
                                  "FROM access_control access_control " &
                                  "INNER JOIN access_control_rule " &
                                  "access_control_rule ON access_control.[ac_id] = access_control_rule.[ac_id] " &
                                  "WHERE access_control_rule.[user id] = '" & cUserId.Trim & "' AND " &
                                  "access_control.[parent] = '" & cParent.Trim & "' AND " &
                                  "access_control.[process] = '" & cProcess.Trim & "'"

                Dim rDr As SqlDataReader = Cmd.ExecuteReader
                While rDr.Read
                    If rDr.Item(0).ToString.Trim = "Y" Then
                        lRoles = True
                    Else
                        lRoles = False
                    End If
                End While

                If rDr.HasRows = False Then
                    lRoles = False
                End If

                rDr.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return lRoles
    End Function
End Module
