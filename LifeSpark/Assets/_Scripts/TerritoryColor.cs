using UnityEngine;
using System.Collections;

public class TerritoryColor : MonoBehaviour
{

    private GameObject sparkPointA0;
    private GameObject sparkPointA1;
    private GameObject sparkPointA3;
    private GameObject sparkPointA5;
    private GameObject sparkPointA6;
    private GameObject sparkPointB0;
    private GameObject sparkPointB3;
    private GameObject sparkPointB6;
    private GameObject sparkPointC2;
    private GameObject sparkPointC4;
    private GameObject sparkPointD0;
    private GameObject sparkPointD1;
    private GameObject sparkPointD5;
    private GameObject sparkPointD6;
    private GameObject sparkPointE2;
    private GameObject sparkPointE4;
    private GameObject sparkPointF0;
    private GameObject sparkPointF3;
    private GameObject sparkPointF6;
    private GameObject sparkPointG0;
    private GameObject sparkPointG1;
    private GameObject sparkPointG3;
    private GameObject sparkPointG5;
    private GameObject sparkPointG6;
    private Color defaultColor;
    private Color redTeamColor;
    private Color blueTeamColor;
    private Mesh triangleNWmesh;
    private Mesh triangleSWmesh;
    private Mesh triangleSEmesh;
    private Mesh triangleNEmesh;
    private Mesh doublePentagonNWmesh;
    private Mesh doublePentagonSWmesh;
    private Mesh doublePentagonNEmesh;
    private Mesh doublePentagonSEmesh;
    private Mesh quadrilateralNNWmesh;
    private Mesh quadrilateralWNWmesh;
    private Mesh quadrilateralNNEmesh;
    private Mesh quadrilateralENEmesh;
    private Mesh quadrilateralESEmesh;
    private Mesh quadrilateralSSEmesh;
    private Mesh quadrilateralSSWmesh;
    private Mesh quadrilateralWSWmesh;
    private GameObject triangleNWobject;
    private GameObject triangleNEobject;
    private GameObject triangleSEobject;
    private GameObject triangleSWobject;
    private GameObject doublePentagonNWobject;
    private GameObject doublePentagonNEobject;
    private GameObject doublePentagonSWobject;
    private GameObject doublePentagonSEobject;
    private GameObject quadrilateralNNWobject;
    private GameObject quadrilateralWNWobject;
    private GameObject quadrilateralNNEobject;
    private GameObject quadrilateralENEobject;
    private GameObject quadrilateralESEobject;
    private GameObject quadrilateralSSEobject;
    private GameObject quadrilateralSSWobject;
    private GameObject quadrilateralWSWobject;

    // Use this for initialization
    void Start()
    {
        //Get SparkPoints
        sparkPointA0 = GameObject.Find("SparkPointA0");
        sparkPointA1 = GameObject.Find("SparkPointA1");
        sparkPointA3 = GameObject.Find("SparkPointA3");
        sparkPointA5 = GameObject.Find("SparkPointA5");
        sparkPointA6 = GameObject.Find("SparkPointA6");
        sparkPointB0 = GameObject.Find("SparkPointB0");
        sparkPointB3 = GameObject.Find("SparkPointB3");
        sparkPointB6 = GameObject.Find("SparkPointB6");
        sparkPointC2 = GameObject.Find("SparkPointC2");
        sparkPointC4 = GameObject.Find("SparkPointC4");
        sparkPointD0 = GameObject.Find("SparkPointD0");
        sparkPointD1 = GameObject.Find("SparkPointD1");
        sparkPointD5 = GameObject.Find("SparkPointD5");
        sparkPointD6 = GameObject.Find("SparkPointD6");
        sparkPointE2 = GameObject.Find("SparkPointE2");
        sparkPointE4 = GameObject.Find("SparkPointE4");
        sparkPointF0 = GameObject.Find("SparkPointF0");
        sparkPointF3 = GameObject.Find("SparkPointF3");
        sparkPointF6 = GameObject.Find("SparkPointF6");
        sparkPointG0 = GameObject.Find("SparkPointG0");
        sparkPointG1 = GameObject.Find("SparkPointG1");
        sparkPointG3 = GameObject.Find("SparkPointG3");
        sparkPointG5 = GameObject.Find("SparkPointG5");
        sparkPointG6 = GameObject.Find("SparkPointG6");

        //Get default SparkPoint color and set team territory colors
        defaultColor = sparkPointA0.renderer.material.color;
        redTeamColor = new Color(139.0f / 255, 0f, 0f);
        blueTeamColor = new Color(0f, 0f, 128.0f / 255);

        //Prepare GameObjects
        prepareTriangleNW();
        prepareTriangleNE();
        prepareTriangleSE();
        prepareTriangleSW();
        prepareDoublePentagonNW();
        prepareDoublePentagonNE();
        prepareDoublePentagonSE();
        prepareDoublePentagonSW();
        prepareQuadrilateralNNW();
        prepareQuadrilateralWNW();
        prepareQuadrilateralNNE();
        prepareQuadrilateralENE();
        prepareQuadrilateralSSE();
        prepareQuadrilateralESE();
        prepareQuadrilateralSSW();
        prepareQuadrilateralWSW();
    }

    // Update is called once per frame
    void Update()
    {
        //NW Corner
        if (sparkPointA0.gameObject == null || sparkPointA1.gameObject == null || sparkPointB0.gameObject == null)
        {
            //Remove Triangle
            triangleNWmesh.Clear();
        }
        else if (sparkPointA0.renderer.material.color == sparkPointA1.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointB0.renderer.material.color && sparkPointA0.renderer.material.color != defaultColor)
        {
            //Draw Triangle
            if (sparkPointA0.renderer.material.color == Color.red)
            {
                triangleNWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA0.renderer.material.color == Color.blue)
            {
                triangleNWobject.renderer.material.color = blueTeamColor;
            }
            triangleNWmesh.triangles = new int[] { 0, 1, 2 };
            triangleNWmesh.RecalculateNormals();
            triangleNWmesh.RecalculateBounds();
            triangleNWmesh.Optimize();
        }
        if (sparkPointA1.gameObject == null || sparkPointA3.gameObject == null || sparkPointB3.gameObject == null || sparkPointC2.gameObject == null || sparkPointD1.gameObject == null || sparkPointD0.gameObject == null || sparkPointB0.gameObject == null)
        {
            //Remove Double Pentagon
            doublePentagonNWmesh.Clear();
        }
        else if (sparkPointA1.renderer.material.color == sparkPointA3.renderer.material.color && sparkPointA1.renderer.material.color == sparkPointB3.renderer.material.color && sparkPointA1.renderer.material.color == sparkPointC2.renderer.material.color && sparkPointA1.renderer.material.color == sparkPointD1.renderer.material.color && sparkPointA1.renderer.material.color == sparkPointD0.renderer.material.color && sparkPointA1.renderer.material.color == sparkPointB0.renderer.material.color && sparkPointA1.renderer.material.color != defaultColor)
        {
            //Draw Double Pentagon
            if (sparkPointA1.renderer.material.color == Color.red)
            {
                doublePentagonNWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA1.renderer.material.color == Color.blue)
            {
                doublePentagonNWobject.renderer.material.color = blueTeamColor;
            }
            doublePentagonNWmesh.triangles = new int[] { 0, 1, 2, 2, 3, 0, 0, 3, 6, 6, 3, 4, 6, 4, 5 };
            doublePentagonNWmesh.RecalculateNormals();
            doublePentagonNWmesh.RecalculateBounds();
            doublePentagonNWmesh.Optimize();
        }
        if (sparkPointA0.gameObject == null || sparkPointA1.gameObject == null || sparkPointA3.gameObject == null || sparkPointB3.gameObject == null || sparkPointC2.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralNNWmesh.Clear();
        }
        else if (sparkPointA0.renderer.material.color == sparkPointA1.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointA3.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointB3.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointC2.renderer.material.color && sparkPointA0.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointA1.renderer.material.color == Color.red)
            {
                quadrilateralNNWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA1.renderer.material.color == Color.blue)
            {
                quadrilateralNNWobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralNNWmesh.triangles = new int[] { 0, 2, 3, 0, 3, 4 };
            quadrilateralNNWmesh.RecalculateNormals();
            quadrilateralNNWmesh.RecalculateBounds();
            quadrilateralNNWmesh.Optimize();
        }
        if (sparkPointA0.gameObject == null || sparkPointC2.gameObject == null || sparkPointD1.gameObject == null || sparkPointD0.gameObject == null || sparkPointB0.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralWNWmesh.Clear();
        }
        else if (sparkPointA0.renderer.material.color == sparkPointB0.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointD0.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointD1.renderer.material.color && sparkPointA0.renderer.material.color == sparkPointC2.renderer.material.color && sparkPointA0.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointA0.renderer.material.color == Color.red)
            {
                quadrilateralWNWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA0.renderer.material.color == Color.blue)
            {
                quadrilateralWNWobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralWNWmesh.triangles = new int[] { 0, 1, 3, 1, 2, 3 };
            quadrilateralWNWmesh.RecalculateNormals();
            quadrilateralWNWmesh.RecalculateBounds();
            quadrilateralWNWmesh.Optimize();
        }
        


        //NE Corner
        if (sparkPointA6.gameObject == null || sparkPointA5.gameObject == null || sparkPointB6.gameObject == null)
        {
            //Remove Triangle
            triangleNEmesh.Clear();
        }
        else if (sparkPointA6.renderer.material.color == sparkPointA5.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointB6.renderer.material.color && 
                 sparkPointA6.renderer.material.color != defaultColor)
        {
            //Draw Triangle
            if (sparkPointA6.renderer.material.color == Color.red)
            {
                triangleNEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA6.renderer.material.color == Color.blue)
            {
                triangleNEobject.renderer.material.color = blueTeamColor;
            }
            triangleNEmesh.triangles = new int[] { 2, 1, 0 };
            triangleNEmesh.RecalculateNormals();
            triangleNEmesh.RecalculateBounds();
            triangleNEmesh.Optimize();
        }
        if (sparkPointA5.gameObject == null || sparkPointA3.gameObject == null || 
            sparkPointB3.gameObject == null || sparkPointC4.gameObject == null || 
            sparkPointD5.gameObject == null || sparkPointD6.gameObject == null || 
            sparkPointB6.gameObject == null)
        {
            //Remove Double Pentagon
            doublePentagonNEmesh.Clear();
        }
        else if (sparkPointA5.renderer.material.color == sparkPointA3.renderer.material.color && 
                 sparkPointA5.renderer.material.color == sparkPointB3.renderer.material.color && 
                 sparkPointA5.renderer.material.color == sparkPointC4.renderer.material.color && 
                 sparkPointA5.renderer.material.color == sparkPointD5.renderer.material.color && 
                 sparkPointA5.renderer.material.color == sparkPointD6.renderer.material.color && 
                 sparkPointA5.renderer.material.color == sparkPointB6.renderer.material.color && 
                 sparkPointA5.renderer.material.color != defaultColor)
        {
            //Draw Double Pentagon
            if (sparkPointA5.renderer.material.color == Color.red)
            {
                doublePentagonNEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA5.renderer.material.color == Color.blue)
            {
                doublePentagonNEobject.renderer.material.color = blueTeamColor;
            }
            doublePentagonNEmesh.triangles = new int[] { 5, 4, 6, 4, 3, 6, 6, 3, 0, 0, 3, 2, 2, 1, 0 };
            doublePentagonNEmesh.RecalculateNormals();
            doublePentagonNEmesh.RecalculateBounds();
            doublePentagonNEmesh.Optimize();
        }
        if (sparkPointA6.gameObject == null || 
            sparkPointA5.gameObject == null || 
            sparkPointA3.gameObject == null || 
            sparkPointB3.gameObject == null || 
            sparkPointC4.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralNNEmesh.Clear();
        }
        else if (sparkPointA6.renderer.material.color == sparkPointA5.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointA3.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointB3.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointC4.renderer.material.color && 
                 sparkPointA6.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointA5.renderer.material.color == Color.red)
            {
                quadrilateralNNEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA5.renderer.material.color == Color.blue)
            {
                quadrilateralNNEobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralNNEmesh.triangles = new int[] { 4, 3, 0, 3, 2, 0};
            quadrilateralNNEmesh.RecalculateNormals();
            quadrilateralNNEmesh.RecalculateBounds();
            quadrilateralNNEmesh.Optimize();
        }
        if (sparkPointA6.gameObject == null || 
            sparkPointC4.gameObject == null || 
            sparkPointD5.gameObject == null || 
            sparkPointD6.gameObject == null || 
            sparkPointB6.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralENEmesh.Clear();
        }
        else if (sparkPointA6.renderer.material.color == sparkPointB6.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointD6.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointD5.renderer.material.color && 
                 sparkPointA6.renderer.material.color == sparkPointC4.renderer.material.color && 
                 sparkPointA6.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointA6.renderer.material.color == Color.red)
            {
                quadrilateralENEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointA6.renderer.material.color == Color.blue)
            {
                quadrilateralENEobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralENEmesh.triangles = new int[] {3, 2, 1, 3, 1, 0 };
            quadrilateralENEmesh.RecalculateNormals();
            quadrilateralENEmesh.RecalculateBounds();
            quadrilateralENEmesh.Optimize();
        }


        //SE Corner
        if (sparkPointG6.gameObject == null || sparkPointG5.gameObject == null || sparkPointF6.gameObject == null)
        {
            //Remove Triangle
            triangleSEmesh.Clear();
        }
        else if (sparkPointG6.renderer.material.color == sparkPointG5.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointF6.renderer.material.color &&
                 sparkPointG6.renderer.material.color != defaultColor)
        {
            //Draw Triangle
            if (sparkPointG6.renderer.material.color == Color.red)
            {
                triangleSEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG6.renderer.material.color == Color.blue)
            {
                triangleSEobject.renderer.material.color = blueTeamColor;
            }
            triangleSEmesh.triangles = new int[] { 0, 1, 2 };
            triangleSEmesh.RecalculateNormals();
            triangleSEmesh.RecalculateBounds();
            triangleSEmesh.Optimize();
        }
        if (sparkPointG5.gameObject == null || sparkPointG3.gameObject == null ||
            sparkPointF3.gameObject == null || sparkPointE4.gameObject == null ||
            sparkPointD5.gameObject == null || sparkPointD6.gameObject == null ||
            sparkPointF6.gameObject == null)
        {
            //Remove Double Pentagon
            doublePentagonSEmesh.Clear();
        }
        else if (sparkPointG5.renderer.material.color == sparkPointG3.renderer.material.color &&
                 sparkPointG5.renderer.material.color == sparkPointF3.renderer.material.color &&
                 sparkPointG5.renderer.material.color == sparkPointE4.renderer.material.color &&
                 sparkPointG5.renderer.material.color == sparkPointD5.renderer.material.color &&
                 sparkPointG5.renderer.material.color == sparkPointD6.renderer.material.color &&
                 sparkPointG5.renderer.material.color == sparkPointF6.renderer.material.color &&
                 sparkPointG5.renderer.material.color != defaultColor)
        {
            //Draw Double Pentagon
            if (sparkPointG5.renderer.material.color == Color.red)
            {
                doublePentagonSEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG5.renderer.material.color == Color.blue)
            {
                doublePentagonSEobject.renderer.material.color = blueTeamColor;
            }
            doublePentagonSEmesh.triangles = new int[] { 0, 1, 2, 2, 3, 0, 0, 3, 6, 6, 3, 4, 6, 4, 5 }; 
            doublePentagonSEmesh.RecalculateNormals();
            doublePentagonSEmesh.RecalculateBounds();
            doublePentagonSEmesh.Optimize();
        }
        if (sparkPointG6.gameObject == null ||
            sparkPointG5.gameObject == null ||
            sparkPointG3.gameObject == null ||
            sparkPointF3.gameObject == null ||
            sparkPointE4.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralSSEmesh.Clear();
        }
        else if (sparkPointG6.renderer.material.color == sparkPointG5.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointG3.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointF3.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointE4.renderer.material.color &&
                 sparkPointG6.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointG5.renderer.material.color == Color.red)
            {
                quadrilateralSSEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG5.renderer.material.color == Color.blue)
            {
                quadrilateralSSEobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralSSEmesh.triangles = new int[] { 0, 2, 3, 0, 3, 4 };
            quadrilateralSSEmesh.RecalculateNormals();
            quadrilateralSSEmesh.RecalculateBounds();
            quadrilateralSSEmesh.Optimize();
        }
        if (sparkPointG6.gameObject == null ||
            sparkPointE4.gameObject == null ||
            sparkPointD5.gameObject == null ||
            sparkPointD6.gameObject == null ||
            sparkPointF6.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralESEmesh.Clear();
        }
        else if (sparkPointG6.renderer.material.color == sparkPointF6.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointD6.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointD5.renderer.material.color &&
                 sparkPointG6.renderer.material.color == sparkPointE4.renderer.material.color &&
                 sparkPointG6.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointG6.renderer.material.color == Color.red)
            {
                quadrilateralESEobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG6.renderer.material.color == Color.blue)
            {
                quadrilateralESEobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralESEmesh.triangles = new int[] { 0, 1, 3, 1, 2, 3 };
            quadrilateralESEmesh.RecalculateNormals();
            quadrilateralESEmesh.RecalculateBounds();
            quadrilateralESEmesh.Optimize();
        }


        //SW Corner
        if (sparkPointG0.gameObject == null || sparkPointG1.gameObject == null || sparkPointF0.gameObject == null)
        {
            //Remove Triangle
            triangleSWmesh.Clear();
        }
        else if (sparkPointG0.renderer.material.color == sparkPointG1.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointF0.renderer.material.color &&
                 sparkPointG0.renderer.material.color != defaultColor)
        {
            //Draw Triangle
            if (sparkPointG0.renderer.material.color == Color.red)
            {
                triangleSWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG0.renderer.material.color == Color.blue)
            {
                triangleSWobject.renderer.material.color = blueTeamColor;
            }
            triangleSWmesh.triangles = new int[] { 2, 1, 0 };
            triangleSWmesh.RecalculateNormals();
            triangleSWmesh.RecalculateBounds();
            triangleSWmesh.Optimize();
        }
        if (sparkPointG1.gameObject == null || sparkPointG3.gameObject == null ||
            sparkPointF3.gameObject == null || sparkPointE2.gameObject == null ||
            sparkPointD1.gameObject == null || sparkPointD0.gameObject == null ||
            sparkPointF0.gameObject == null)
        {
            //Remove Double Pentagon
            doublePentagonSWmesh.Clear();
        }
        else if (sparkPointG1.renderer.material.color == sparkPointG3.renderer.material.color &&
                 sparkPointG1.renderer.material.color == sparkPointF3.renderer.material.color &&
                 sparkPointG1.renderer.material.color == sparkPointE2.renderer.material.color &&
                 sparkPointG1.renderer.material.color == sparkPointD1.renderer.material.color &&
                 sparkPointG1.renderer.material.color == sparkPointD0.renderer.material.color &&
                 sparkPointG1.renderer.material.color == sparkPointF0.renderer.material.color &&
                 sparkPointG1.renderer.material.color != defaultColor)
        {
            //Draw Double Pentagon
            if (sparkPointG1.renderer.material.color == Color.red)
            {
                doublePentagonSWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG1.renderer.material.color == Color.blue)
            {
                doublePentagonSWobject.renderer.material.color = blueTeamColor;
            }
            doublePentagonSWmesh.triangles = new int[] { 5, 4, 6, 4, 3, 6, 6, 3, 0, 0, 3, 2, 2, 1, 0 };
            doublePentagonSWmesh.RecalculateNormals();
            doublePentagonSWmesh.RecalculateBounds();
            doublePentagonSWmesh.Optimize();
        }
        if (sparkPointG0.gameObject == null ||
            sparkPointG1.gameObject == null ||
            sparkPointG3.gameObject == null ||
            sparkPointF3.gameObject == null ||
            sparkPointE2.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralSSWmesh.Clear();
        }
        else if (sparkPointG0.renderer.material.color == sparkPointG1.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointG3.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointF3.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointE2.renderer.material.color &&
                 sparkPointG0.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointG1.renderer.material.color == Color.red)
            {
                quadrilateralSSWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG1.renderer.material.color == Color.blue)
            {
                quadrilateralSSWobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralSSWmesh.triangles = new int[] { 4, 3, 0, 3, 2, 0 };
            quadrilateralSSWmesh.RecalculateNormals();
            quadrilateralSSWmesh.RecalculateBounds();
            quadrilateralSSWmesh.Optimize();
        }
        if (sparkPointG0.gameObject == null ||
            sparkPointE2.gameObject == null ||
            sparkPointD1.gameObject == null ||
            sparkPointD0.gameObject == null ||
            sparkPointF0.gameObject == null)
        {
            //Remove Quadrilateral
            quadrilateralWSWmesh.Clear();
        }
        else if (sparkPointG0.renderer.material.color == sparkPointF0.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointD0.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointD1.renderer.material.color &&
                 sparkPointG0.renderer.material.color == sparkPointE2.renderer.material.color &&
                 sparkPointG0.renderer.material.color != defaultColor)
        {
            //Draw Quadrilateral
            if (sparkPointG0.renderer.material.color == Color.red)
            {
                quadrilateralWSWobject.renderer.material.color = redTeamColor;
            }
            else if (sparkPointG0.renderer.material.color == Color.blue)
            {
                quadrilateralWSWobject.renderer.material.color = blueTeamColor;
            }
            quadrilateralWSWmesh.triangles = new int[] { 3, 2, 1, 3, 1, 0 };
            quadrilateralWSWmesh.RecalculateNormals();
            quadrilateralWSWmesh.RecalculateBounds();
            quadrilateralWSWmesh.Optimize();
        }

    }




    // ***Polygon Initialization Functions***

    // NW Triangle
    private void prepareTriangleNW()
    {
        triangleNWobject = new GameObject();
        var MF = (MeshFilter)triangleNWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)triangleNWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        triangleNWmesh = new Mesh();
        triangleNWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA0.transform.position.z),
			new Vector3(sparkPointA1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA1.transform.position.z), 
			new Vector3(sparkPointB0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB0.transform.position.z)
		};
        triangleNWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1) };
        MF.mesh = triangleNWmesh;
    }

    // NE Triangle
    private void prepareTriangleNE()
    {
        triangleNEobject = new GameObject();
        var MF = (MeshFilter)triangleNEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)triangleNEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        triangleNEmesh = new Mesh();
        triangleNEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA6.transform.position.z),
			new Vector3(sparkPointA5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA5.transform.position.z), 
			new Vector3(sparkPointB6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB6.transform.position.z)
		};
        triangleNEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1) };
        MF.mesh = triangleNEmesh;
    }

    // SE Triangle
    private void prepareTriangleSE()
    {
        triangleSEobject = new GameObject();
        var MF = (MeshFilter)triangleSEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)triangleSEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        triangleSEmesh = new Mesh();
        triangleSEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG6.transform.position.z),
			new Vector3(sparkPointG5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG5.transform.position.z), 
			new Vector3(sparkPointF6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF6.transform.position.z)
		};
        triangleSEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1) };
        MF.mesh = triangleSEmesh;
    }

    // SW Triangle
    private void prepareTriangleSW()
    {
        triangleSWobject = new GameObject();
        var MF = (MeshFilter)triangleSWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)triangleSWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        triangleSWmesh = new Mesh();
        triangleSWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG0.transform.position.z),
			new Vector3(sparkPointG1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG1.transform.position.z), 
			new Vector3(sparkPointF0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF0.transform.position.z)
		};
        triangleSWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1) };
        MF.mesh = triangleSWmesh;
    }

    // NW Double Pentagon
    private void prepareDoublePentagonNW()
    {
        doublePentagonNWobject = new GameObject();
        var MF = (MeshFilter)doublePentagonNWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)doublePentagonNWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        doublePentagonNWmesh = new Mesh();
        doublePentagonNWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA0.transform.position.z),
			new Vector3(sparkPointA3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA1.transform.position.z), 
			new Vector3(sparkPointB3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB3.transform.position.z),
			new Vector3(sparkPointC2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC2.transform.position.z),
			new Vector3(sparkPointD1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD1.transform.position.z),
			new Vector3(sparkPointD0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD0.transform.position.z),
			new Vector3(sparkPointB0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB0.transform.position.z)
		};
        doublePentagonNWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = doublePentagonNWmesh;
    }

    // NE Double Pentagon
    private void prepareDoublePentagonNE()
    {
        doublePentagonNEobject = new GameObject();
        var MF = (MeshFilter)doublePentagonNEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)doublePentagonNEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        doublePentagonNEmesh = new Mesh();
        doublePentagonNEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA5.transform.position.z),
			new Vector3(sparkPointA3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA3.transform.position.z), 
			new Vector3(sparkPointB3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB3.transform.position.z),
			new Vector3(sparkPointC4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC4.transform.position.z),
			new Vector3(sparkPointD5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD5.transform.position.z),
			new Vector3(sparkPointD6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD6.transform.position.z),
			new Vector3(sparkPointB6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB6.transform.position.z)
		};
        doublePentagonNEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = doublePentagonNEmesh;
    }

    // SE Double Pentagon
    private void prepareDoublePentagonSE()
    {
        doublePentagonSEobject = new GameObject();
        var MF = (MeshFilter)doublePentagonSEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)doublePentagonSEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        doublePentagonSEmesh = new Mesh();
        doublePentagonSEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG5.transform.position.z),
			new Vector3(sparkPointG3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG3.transform.position.z), 
			new Vector3(sparkPointF3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF3.transform.position.z),
			new Vector3(sparkPointE4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE4.transform.position.z),
			new Vector3(sparkPointD5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD5.transform.position.z),
			new Vector3(sparkPointD6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD6.transform.position.z),
			new Vector3(sparkPointF6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF6.transform.position.z)
		};
        doublePentagonSEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = doublePentagonSEmesh;
    }

    // SW Double Pentagon
    private void prepareDoublePentagonSW()
    {
        doublePentagonSWobject = new GameObject();
        var MF = (MeshFilter)doublePentagonSWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)doublePentagonSWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        doublePentagonSWmesh = new Mesh();
        doublePentagonSWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG1.transform.position.z),
			new Vector3(sparkPointG3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG3.transform.position.z), 
			new Vector3(sparkPointF3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF3.transform.position.z),
			new Vector3(sparkPointE2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE2.transform.position.z),
			new Vector3(sparkPointD1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD1.transform.position.z),
			new Vector3(sparkPointD0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD0.transform.position.z),
			new Vector3(sparkPointF0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF0.transform.position.z)
		};
        doublePentagonSWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = doublePentagonSWmesh;
    }

    // NNW Quadrilateral
    private void prepareQuadrilateralNNW()
    {
        quadrilateralNNWobject = new GameObject();
        var MF = (MeshFilter)quadrilateralNNWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralNNWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralNNWmesh = new Mesh();
        quadrilateralNNWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA0.transform.position.z),
			new Vector3(sparkPointA1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA1.transform.position.z), 
			new Vector3(sparkPointA3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA3.transform.position.z),
			new Vector3(sparkPointB3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB3.transform.position.z),
			new Vector3(sparkPointC2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC2.transform.position.z)
		};
        quadrilateralNNWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralNNWmesh;
    }

    // WNW Quadrilateral
    private void prepareQuadrilateralWNW()
    {
        quadrilateralWNWobject = new GameObject();
        var MF = (MeshFilter)quadrilateralWNWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralWNWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralWNWmesh = new Mesh();
        quadrilateralWNWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA0.transform.position.z),
			new Vector3(sparkPointC2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC2.transform.position.z), 
			new Vector3(sparkPointD1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD1.transform.position.z),
			new Vector3(sparkPointD0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD0.transform.position.z),
			new Vector3(sparkPointB0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB0.transform.position.z)
		};
        quadrilateralWNWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralWNWmesh;
    }

    // NNE Quadrilateral
    private void prepareQuadrilateralNNE()
    {
        quadrilateralNNEobject = new GameObject();
        var MF = (MeshFilter)quadrilateralNNEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralNNEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralNNEmesh = new Mesh();
        quadrilateralNNEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA6.transform.position.z),
			new Vector3(sparkPointA5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA5.transform.position.z), 
			new Vector3(sparkPointA3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA3.transform.position.z),
			new Vector3(sparkPointB3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB3.transform.position.z),
			new Vector3(sparkPointC4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC4.transform.position.z)
		};
        quadrilateralNNEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralNNEmesh;
    }

    // ENE Quadrilateral
    private void prepareQuadrilateralENE()
    {
        quadrilateralENEobject = new GameObject();
        var MF = (MeshFilter)quadrilateralENEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralENEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralENEmesh = new Mesh();
        quadrilateralENEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointA6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointA6.transform.position.z),
			new Vector3(sparkPointC4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointC4.transform.position.z), 
			new Vector3(sparkPointD5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD5.transform.position.z),
			new Vector3(sparkPointD6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD6.transform.position.z),
			new Vector3(sparkPointB6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointB6.transform.position.z)
		};
        quadrilateralENEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralENEmesh;
    }

    // SSE Quadrilateral
    private void prepareQuadrilateralSSE()
    {
        quadrilateralSSEobject = new GameObject();
        var MF = (MeshFilter)quadrilateralSSEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralSSEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralSSEmesh = new Mesh();
        quadrilateralSSEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG6.transform.position.z),
			new Vector3(sparkPointG5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG5.transform.position.z), 
			new Vector3(sparkPointG3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG3.transform.position.z),
			new Vector3(sparkPointF3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF3.transform.position.z),
			new Vector3(sparkPointE4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE4.transform.position.z)
		};
        quadrilateralSSEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralSSEmesh;
    }

    // ESE Quadrilateral
    private void prepareQuadrilateralESE()
    {
        quadrilateralESEobject = new GameObject();
        var MF = (MeshFilter)quadrilateralESEobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralESEobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralESEmesh = new Mesh();
        quadrilateralESEmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG6.transform.position.z),
			new Vector3(sparkPointE4.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE4.transform.position.z), 
			new Vector3(sparkPointD5.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD5.transform.position.z),
			new Vector3(sparkPointD6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD6.transform.position.z),
			new Vector3(sparkPointF6.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF6.transform.position.z)
		};
        quadrilateralESEmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralESEmesh;
    }

    // SSW Quadrilateral
    private void prepareQuadrilateralSSW()
    {
        quadrilateralSSWobject = new GameObject();
        var MF = (MeshFilter)quadrilateralSSWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralSSWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralSSWmesh = new Mesh();
        quadrilateralSSWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG0.transform.position.z),
			new Vector3(sparkPointG1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG1.transform.position.z), 
			new Vector3(sparkPointG3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG3.transform.position.z),
			new Vector3(sparkPointF3.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF3.transform.position.z),
			new Vector3(sparkPointE2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE2.transform.position.z)
		};
        quadrilateralSSWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralSSWmesh;
    }

    // WSW Quadrilateral
    private void prepareQuadrilateralWSW()
    {
        quadrilateralWSWobject = new GameObject();
        var MF = (MeshFilter)quadrilateralWSWobject.AddComponent("MeshFilter");
        var MR = (MeshRenderer)quadrilateralWSWobject.AddComponent("MeshRenderer");
        MR.material = this.renderer.material;
        quadrilateralWSWmesh = new Mesh();
        quadrilateralWSWmesh.vertices = new Vector3[] {
			new Vector3(sparkPointG0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointG0.transform.position.z),
			new Vector3(sparkPointE2.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointE2.transform.position.z), 
			new Vector3(sparkPointD1.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD1.transform.position.z),
			new Vector3(sparkPointD0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointD0.transform.position.z),
			new Vector3(sparkPointF0.transform.position.x, this.gameObject.transform.position.y + 0.005f, sparkPointF0.transform.position.z)
		};
        quadrilateralWSWmesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
        MF.mesh = quadrilateralWSWmesh;
    }
}

