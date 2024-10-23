Public Class Form2
    Private zoomFactor As Single = 1.0
    Private dragging As Boolean = False
    Private dragStart As Point
    Private originalImage As Image ' Store the original image for better quality

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the image into the PictureBox from the specified path
        originalImage = Image.FromFile("C:\Users\Mark\source\repos\Campus-Compass-CamPASS--master\Resources\Map of elida 1.jpg")
        PictureBox2.Image = originalImage
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.Size = New Size(PictureBox2.Image.Width * zoomFactor, PictureBox2.Image.Height * zoomFactor)
    End Sub

    Private Sub Form3_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        ' Zoom in or out based on the mouse wheel movement
        If e.Delta > 0 Then
            zoomFactor += 0.1F ' Zoom in
        Else
            zoomFactor -= 0.1F ' Zoom out
        End If

        zoomFactor = Math.Max(0.1F, zoomFactor) ' Prevent zooming out too far

        ' Redraw the image with the new zoom factor
        PictureBox2.Image = New Bitmap(originalImage, New Size(originalImage.Width * zoomFactor, originalImage.Height * zoomFactor))
        PictureBox2.Size = New Size(PictureBox2.Image.Width, PictureBox2.Image.Height)
        PictureBox2.Invalidate()
    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        If e.Button = MouseButtons.Left Then
            dragging = True
            dragStart = e.Location
        End If
    End Sub

    Private Sub PictureBox2_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseMove
        If dragging Then
            Dim dx As Integer = e.Location.X - dragStart.X
            Dim dy As Integer = e.Location.Y - dragStart.Y
            PictureBox2.Left += dx
            PictureBox2.Top += dy
        End If
    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp
        dragging = False
    End Sub
End Class
