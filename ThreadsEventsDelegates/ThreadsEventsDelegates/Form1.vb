Imports System.Threading

Public Class Form1
    Private r As New Random
    Private sleepMax As Int16 = 5
    Private thread_1 As Thread
    Private thread_2 As Thread
    Private thisLock As New Object
    ' Create the token source for Task cancellation.
    Private cts1 As CancellationTokenSource = New CancellationTokenSource()
    Private cts2 As CancellationTokenSource = New CancellationTokenSource()
    Private Sub Button_start_Click(sender As Object, e As EventArgs) Handles Button_start.Click
        AddHandler ReportLeader, AddressOf Race_ReportLeader ' another way of line 19.
        thread_1 = New Thread(AddressOf RedBox)
        thread_2 = New Thread(AddressOf BlueBox)
        thread_1.Start(cts1.Token)
        thread_2.Start(cts2.Token)
    End Sub

    Public Event ReportLeader(ByVal leaderColor As String)
    Public Event ReportWinner(ByVal winnerColor As String)

    Public Delegate Sub update_ui(ByVal val As String)
    Dim mut As Mutex = New Mutex()
    Private Sub update_Control(ByVal text As String)
        mut.WaitOne()
        Label1.Text = $"{text} win"
        mut.ReleaseMutex()
    End Sub
    Private Sub Race_ReportWinner(ByVal winnerColor As String)
        Dim updateLabel1 As update_ui = New update_ui(AddressOf update_Control)
        Label1.Invoke(updateLabel1, winnerColor)
        If winnerColor = "RedBox" Then
            cts2.Cancel() 'cuncel bluebox 1st.
            cts1.Cancel()
            'thread_2.Abort()
            'thread_1.Abort()
        ElseIf winnerColor = "BlueBox" Then
            cts1.Cancel()
            cts2.Cancel()
        End If
    End Sub
    Private Sub Race_ReportLeader(ByVal leaderColor As String) 'Handles Me.ReportLeader
        Dim G As Graphics = Me.CreateGraphics
        SyncLock Me
            G.FillRectangle(New SolidBrush(Me.BackColor), New Rectangle(New Point(350, 300), New Size(200, 100)))
            G.DrawString($"{leaderColor} lead", New Font("Arial", 14), New SolidBrush(Color.Black), New PointF(350, 300))
            'Label1.Text = leaderColor
            'Label1.BeginInvoke(Sub() Label1.Text = leaderColor)
        End SyncLock
    End Sub
    Dim xR As Int16 = 100
    Dim redLead As Boolean = False
    Private Sub RedBox(ByVal obj As Object)
        If obj Is Nothing Then Return
        Dim token As CancellationToken = obj
        Dim G As Graphics = Me.CreateGraphics
        Try
            Do While xR < 650
                If token.IsCancellationRequested Then
                    Debug.WriteLine("Cancellation has been requested...")
                    ' Perform cleanup if necessary.
                    '...
                    G.FillRectangle(New SolidBrush(Color.Red), New Rectangle(New Point(xR, 100), New Size(25, 25)))
                    ' Terminate the operation.
                    Return
                End If
                G.FillRectangle(New SolidBrush(Color.Red), New Rectangle(New Point(xR, 100), New Size(25, 25)))
                Thread.Sleep(r.Next(20, 50))
                G.FillRectangle(New SolidBrush(Me.BackColor), New Rectangle(New Point(xR, 100), New Size(25, 25)))
                xR += r.Next(sleepMax)
                If xR > xB And redLead.Equals(False) Then
                    redLead = True
                    blueLead = False
                    RaiseEvent ReportLeader("RedBox")
                End If
            Loop
            G.FillRectangle(New SolidBrush(Color.Red), New Rectangle(New Point(xR, 100), New Size(25, 25)))
            RaiseEvent ReportWinner("RedBox")
        Catch ex As Exception
            G.FillRectangle(New SolidBrush(Color.Red), New Rectangle(New Point(xR, 100), New Size(25, 25)))
        End Try
    End Sub
    Dim xB As Int16 = 100
    Dim blueLead As Boolean = False
    Private Sub BlueBox(ByVal obj As Object)
        If obj Is Nothing Then Return
        Dim token As CancellationToken = obj
        Dim G As Graphics = Me.CreateGraphics
        Try
            Do While xB < 650
                If token.IsCancellationRequested Then
                    Debug.WriteLine("Cancellation has been requested...")
                    G.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(New Point(xB, 200), New Size(25, 25)))
                    Return
                End If
                G.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(New Point(xB, 200), New Size(25, 25)))
                Thread.Sleep(r.Next(25, 50))
                G.FillRectangle(New SolidBrush(Me.BackColor), New Rectangle(New Point(xB, 200), New Size(25, 25)))
                xB += r.Next(sleepMax)
                If xB > xR And blueLead.Equals(False) Then
                    redLead = False
                    blueLead = True
                    RaiseEvent ReportLeader("BlueBox")
                End If
            Loop
            G.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(New Point(xB, 200), New Size(25, 25)))
            RaiseEvent ReportWinner("BlueBox")
        Catch ex As Exception
            G.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(New Point(xB, 200), New Size(25, 25)))
        End Try
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim G As Graphics = Me.CreateGraphics
        G.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(New Point(100, 200), New Size(25, 25)))
        G.FillRectangle(New SolidBrush(Color.Red), New Rectangle(New Point(100, 100), New Size(25, 25)))
        G.DrawLine(New Pen(Brushes.Black, 5), New Point(660, 80), New Point(660, 250))
    End Sub
End Class
