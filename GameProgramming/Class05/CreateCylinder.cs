using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cylinder : MonoBehaviour {
List<Vector3> Vertics = new List<Vector3>();
List<int> TriList = new List<int>();

float height = 5f;
float radius = 1f;
int segment = 30;



void Start () {

    Mesh MyMesh = new Mesh();
    Vector3 Pos = gameObject.transform.position;

    int LastIndex = segment * 2 +1 ;
    CreateVirtecs();

    int j = 0;
    while (j < segment)
    {
        TriList.Add(0);
        TriList.Add(j+1);
        TriList.Add(j);
        j++;
    }
    TriList.Add(0);
    TriList.Add(1);
    TriList.Add(segment);




    j = 1;
    while (j < segment)
    {
        TriList.Add(j);
        TriList.Add(j+1);
        TriList.Add(j+segment);

        TriList.Add(j + 1);
        TriList.Add(j + 1 +segment);
        TriList.Add(j + segment);
        j++;
    }
    TriList.Add(segment);
    TriList.Add(1);
    TriList.Add(segment*2);

    TriList.Add(1);
    TriList.Add(segment+1);
    TriList.Add(segment * 2);


    j = segment + 1;
    while (j < LastIndex -1)
    {
        TriList.Add(LastIndex);
        TriList.Add(j);
        TriList.Add(j + 1);

        j++;
    }
    TriList.Add(LastIndex);
    TriList.Add(segment * 2);
    TriList.Add(segment + 1);


    MyMesh.vertices = Vertics.ToArray();

    MyMesh.triangles = TriList.ToArray();
    GetComponent<MeshFilter>().mesh = MyMesh;
}

void CreateVirtecs()
{
    float Nums = 360f / segment;
    Vertics.Add(gameObject.transform.position + new Vector3(0, height *0.5f,0));
    float Cos;
    float Sin;

    for (int i =0; i<segment; i++)
    {
         Cos = Mathf.Cos(i * Mathf.Deg2Rad * Nums);
        Sin  = Mathf.Sin(i * Mathf.Deg2Rad * Nums);
        Vertics.Add(
            new Vector3(Cos * radius,
             gameObject.transform.position.y + 0.5f * height,
             Sin * radius));
    }
    for (int i = 0; i < segment; i++)
    {
        Cos = Mathf.Cos(i * Mathf.Deg2Rad * Nums);
        Sin = Mathf.Sin(i * Mathf.Deg2Rad * Nums);
        Vertics.Add(
            new Vector3(Cos * radius,
             gameObject.transform.position.y - 0.5f * height,
             Sin * radius));
    }

    Vertics.Add(gameObject.transform.position - new Vector3(0, height * 0.5f, 0));

}





// Update is called once per frame
void Update () {

}

}