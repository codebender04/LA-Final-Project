using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FOVCone : MonoBehaviour
{
    public float angle = 60f;
    public float distance = 5f;
    public int segments = 20;
    public LayerMask obstacleMask; // assign walls layer

    Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        int vertexCount = segments + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        float step = angle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = -angle / 2 + step * i;
            float rad = currentAngle * Mathf.Deg2Rad;

            Vector3 dir = transform.rotation * new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, obstacleMask);

            float finalDist = hit ? hit.distance : distance;

            vertices[i + 1] = transform.InverseTransformPoint(
                transform.position + dir * finalDist
            );
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}