Public Class Form3
    Private zoomFactor As Single = 1.0F
    Private isDragging As Boolean = False
    Private lastMousePosition As Point

    ' Event handler for the form load to populate ComboBoxes with hardcoded data
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate ComboBox1 with hardcoded starting points (entrances, library)
        ComboBox1.Items.Add("Entrance")
        ComboBox1.Items.Add("2nd gate")
        ComboBox1.Items.Add("Library")

        ' Populate ComboBox2 with hardcoded destinations (classrooms, library)
        ComboBox2.Items.Add("204")
        ComboBox2.Items.Add("205")
        ComboBox2.Items.Add("Library")
    End Sub

    ' Event handler for the Enter button
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        If ComboBox1.SelectedIndex = -1 Or ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Please select both starting point and destination.")
            Return
        End If
        UpdateImage()
    End Sub

    ' Update the image in the PictureBox based on ComboBox selections
    Private Sub UpdateImage()
        Dim selectedStartingPoint As String = ComboBox1.SelectedItem?.ToString()
        Dim selectedDestination As String = ComboBox2.SelectedItem?.ToString()

        If String.IsNullOrEmpty(selectedStartingPoint) Or String.IsNullOrEmpty(selectedDestination) Then
            MessageBox.Show("Please select both starting point and destination.")
            Return
        End If

        ' Define the image filename based on the selection
        Dim imageFileName As String = ""

        ' Set the filename based on starting point and destination
        If selectedStartingPoint = "Entrance" And selectedDestination = "Library" Then
            imageFileName = "enTOLib.jpg"
        ElseIf selectedStartingPoint = "Entrance" And selectedDestination = "204" Then
            imageFileName = "enTO204.png"
        ElseIf selectedStartingPoint = "Entrance" And selectedDestination = "205" Then
            imageFileName = "enTO205.png"
        Else
            MessageBox.Show("No path found between selected locations.")
            PbCampusMap.Image = Nothing
            Return
        End If

        ' Load the image
        Dim fullImagePath As String = System.IO.Path.Combine("C:\Users\Mark\source\repos\asd\Resources\", imageFileName)
        If System.IO.File.Exists(fullImagePath) Then
            PbCampusMap.Image = Image.FromFile(fullImagePath)
            PbCampusMap.SizeMode = PictureBoxSizeMode.Zoom
        Else
            MessageBox.Show("Image file not found: " & fullImagePath)
            PbCampusMap.Image = Nothing
        End If
    End Sub

    ' Mouse down event for dragging the PictureBox
    Private Sub pbCampusMap_MouseDown(sender As Object, e As MouseEventArgs) Handles PbCampusMap.MouseDown
        If e.Button = MouseButtons.Left Then
            isDragging = True
            lastMousePosition = e.Location
        End If
    End Sub

    ' Mouse move event to drag the PictureBox
    Private Sub pbCampusMap_MouseMove(sender As Object, e As MouseEventArgs) Handles PbCampusMap.MouseMove
        If isDragging Then
            Dim dx As Integer = e.Location.X - lastMousePosition.X
            Dim dy As Integer = e.Location.Y - lastMousePosition.Y
            PbCampusMap.Left += dx
            PbCampusMap.Top += dy
        End If
    End Sub

    ' Mouse up event to stop dragging the PictureBox
    Private Sub pbCampusMap_MouseUp(sender As Object, e As MouseEventArgs) Handles PbCampusMap.MouseUp
        isDragging = False
    End Sub

    ' Mouse wheel event to zoom in and out of the PictureBox
    Private Sub pbCampusMap_MouseWheel(sender As Object, e As MouseEventArgs) Handles PbCampusMap.MouseWheel
        If e.Delta > 0 Then
            zoomFactor += 0.1F ' Zoom in
        Else
            zoomFactor -= 0.1F ' Zoom out
        End If

        zoomFactor = Math.Max(0.1F, zoomFactor) ' Prevent zooming out too much
        If PbCampusMap.Image IsNot Nothing Then
            PbCampusMap.Size = New Size(CInt(PbCampusMap.Image.Width * zoomFactor), CInt(PbCampusMap.Image.Height * zoomFactor))
        End If
    End Sub

    ' Back button to navigate to Form2
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim secondForm As New Form2()

        ' Show Form2
        Form2.Show()
        Me.Hide()
    End Sub

    ' Update the image in PictureBox2 based on ComboBox2 selection (for classroom or library)
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim destination As String = ComboBox2.SelectedItem.ToString()

        ' Define the base path for your images
        Dim basePath As String = "C:\Users\Mark\source\repos\asd\Resources\"

        ' Initialize the image filename based on the destination
        Dim imageFileName As String = ""

        ' Set the filename based on the destination
        Select Case destination
            Case "Library"
                imageFileName = "lib.jpg" ' Filename for Library image
            Case "204"
                imageFileName = "104.jpg" ' Filename for Room 204 image
            Case "205"
                imageFileName = "105.jpg" ' Filename for Room 205 image
            Case Else
                MessageBox.Show("Invalid selection.")
                PictureBox4.Image = Nothing
                Exit Sub
        End Select

        ' Combine the base path and the image filename
        Dim fullImagePath As String = System.IO.Path.Combine(basePath, imageFileName)

        ' Check if the file exists before loading it
        If System.IO.File.Exists(fullImagePath) Then
            PictureBox4.Image = Image.FromFile(fullImagePath)
        Else
            MessageBox.Show("Image file not found: " & fullImagePath)
            PictureBox4.Image = Nothing
        End If
    End Sub
End Class
