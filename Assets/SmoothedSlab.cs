using UnityEngine;

public class SmoothedSlab : MonoBehaviour
{
    public MeshFilter filter;

    void Reset()
    {
        filter = GetComponent<MeshFilter>();
    }

    void Start()
    {
        float h = 0.1f;
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            // bottom face
            new Vector3(-0.5f, -h, -0.5f),
            new Vector3(-0.5f, -h, 0.5f),
            new Vector3(-0.5f, h, -0.5f),
            new Vector3(-0.5f, h, 0.5f),
            // top face
            new Vector3(0.5f, -h, -0.5f),
            new Vector3(0.5f, -h, 0.5f),
            new Vector3(0.5f, h, -0.5f),
            new Vector3(0.5f, h, 0.5f)
        };
        mesh.triangles = new int[]
        {
            // bottom face
            0, 1, 2,
            1, 3, 2,
            // top face
            4, 6, 5,
            5, 6, 7,
            // front face
            0, 4, 1,
            1, 4, 5,
            // back face
            2, 3, 6,
            3, 7, 6,
            // left face
            0, 2, 4,
            2, 6, 4,
            // right face
            1, 5, 3,
            3, 5, 7
        };
        mesh.RecalculateNormals();
        filter.mesh = mesh;
    }
}
