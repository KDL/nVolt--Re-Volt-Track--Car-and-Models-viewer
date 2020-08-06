﻿Imports IrrlichtNETCP

Public Class TexAnimHandler
    Public animation As List(Of Frame)
    Private WithEvents Timer As New Windows.Forms.Timer



    Public animMesh As Mesh
    Public MeshBufferIdx As Integer
    Public firstVxIdx As Integer
    Public isQuad As Boolean

    Dim cur_animidx = 0

    Public Sub New()

    End Sub

    Sub Play()
        Timer.Start()
    End Sub
    Sub [Stop]()
        Timer.Stop()
    End Sub
    Sub vx_swap_routine(idx%, uv As Vector2D)
        Try
            Dim vx = animMesh.GetMeshBuffer(MeshBufferIdx).GetVertex(idx)
            animMesh.GetMeshBuffer(MeshBufferIdx).SetVertex(idx, New Vertex3D(vx.Position, vx.Normal, vx.Color, uv))

        Catch ex As Exception

        End Try
    End Sub
    Sub NextFrame() Handles Timer.Tick
        cur_animidx = (cur_animidx + 1) Mod animation.Count
        Dim curanim = animation(cur_animidx)
        Timer.Interval = Math.Max(1, curanim.Delay)

        animMesh.GetMeshBuffer(MeshBufferIdx).Material.Texture1 = Main.WorldFile.Material(curanim.Tex).Texture1
        animMesh.GetMeshBuffer(MeshBufferIdx).Material.Lighting = False
        vx_swap_routine(firstVxIdx + 0, curanim.UV(0 + 0))
        vx_swap_routine(firstVxIdx + 1, curanim.UV(0 + 1))
        vx_swap_routine(firstVxIdx + 2, curanim.UV(0 + 2))


        If isQuad Then
            vx_swap_routine(firstVxIdx + 3, curanim.UV(0 + 0))
            vx_swap_routine(firstVxIdx + 4, curanim.UV(0 + 2))
            vx_swap_routine(firstVxIdx + 5, curanim.UV(0 + 3))


        End If

    End Sub



End Class
