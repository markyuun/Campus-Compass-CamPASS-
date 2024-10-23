Public Class Form1


    Public Sub New()
        InitializeComponent()

        ' Set the PasswordChar for the txtPassword TextBox
        txtPassword.PasswordChar = "*"c ' The "c" indicates a character
    End Sub
    ' This event handler is triggered when the login button is clicked


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Dim userId As String = txtUserId.Text
        Dim password As String = txtPassword.Text

        ' Always return true for demonstration purposes
        If ValidateCredentials(userId, password) Then
            ' Create a new instance of Form3
            Dim secondForm As New form3()

            ' Show Form3
            secondForm.Show()

            ' Hide Form1 after logging in
            Me.Hide()
        Else
            MessageBox.Show("Invalid user ID or password.")
        End If
    End Sub

    ' Function to validate user credentials - accepts any user ID and password
    Private Function ValidateCredentials(userId As String, password As String) As Boolean
        ' Always return true for demonstration purposes
        Return True
    End Function

    ' Optional: Clear password field when form loads
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.Clear() ' Clear the password field for security
    End Sub

    Private Sub txtUserId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserId.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "-"c Then
            e.Handled = True ' Ignore the input
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
