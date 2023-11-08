<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button_start = New Button()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' Button_start
        ' 
        Button_start.Location = New Point(12, 12)
        Button_start.Name = "Button_start"
        Button_start.Size = New Size(70, 29)
        Button_start.TabIndex = 0
        Button_start.Text = "start"
        Button_start.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.Location = New Point(693, 13)
        Label1.Name = "Label1"
        Label1.Size = New Size(77, 28)
        Label1.TabIndex = 1
        Label1.Text = "winner"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(822, 364)
        Controls.Add(Label1)
        Controls.Add(Button_start)
        Name = "Form1"
        Text = "Racing game"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button_start As Button
    Friend WithEvents Label1 As Label
End Class
