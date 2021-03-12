using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriLib;


public class Import : MonoBehaviour
{
    public GameObject pointFactory;
    List<Vector3> pointList = new List<Vector3>();
    public Camera mainCamera;
    float maxDistance = 10000f;

    void OnDrawGizmos()
    {
     

        float maxDistance = 100;
        RaycastHit hit;
        float radius = 0.5f;
        

        bool isHit = Physics.SphereCast(Camera.main.transform.position, radius, Camera.main.transform.forward, out hit, maxDistance);
        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance);
            Gizmos.DrawWireSphere(Camera.main.transform.position + Camera.main.transform.forward * hit.distance, radius);
        }
        else
        {
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 파일 로드
        using (var assetLoader = new AssetLoader())
        {
            var assetLoaderOptions = GetAssetLoaderOptions();
            var wrapperGameObject = gameObject;
            GameObject myGameObject = assetLoader.LoadFromFileWithTextures("D:\\Unity Hub\\project\\TestImport\\test.stl", assetLoaderOptions, wrapperGameObject);

            //meshObj = FindMeshObject(myGameObject);

            // 파일 이동/회전
            transform.rotation = Quaternion.Euler(new Vector3(-90, 180, 0));
            transform.position = new Vector3(0, 0, 0);

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);


        /*if (Input.GetMouseButton(0))
        {
            
            if (Physics.SphereCast(ray.origin, 0.5f, ray.direction, out hit, maxDistance))
            {
               
                pointList.Add(hit.point);
                if (pointList.Count > 1)
                {
                    ray.origin = hit.point + (new Vector3(0, 0, ray.origin.z ) * Time.deltaTime);
                    ray.direction = ray.origin * -1;
                    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);


                    LineRenderer line = createLine();
                    line.SetPosition(0, pointList[pointList.Count - 2] + (hit.normal * 0.05f * Time.deltaTime));
                    line.SetPosition(1, pointList[pointList.Count - 1] + (hit.normal * 0.05f * Time.deltaTime));
                    
                                       
                    
                }


            }
        }*/


        if (Input.GetMouseButtonDown(0))
        {

            
            if (Physics.SphereCast(ray.origin, 0.5f, ray.direction, out hit, maxDistance))
            {
                // 포인터 위치
                //GameObject point = Instantiate(pointFactory);
                //point.transform.position = hit.point;

                
                //point.transform.position += new Vector3(0.1f, 0, 0) * 5 * Time.deltaTime;
                StartCoroutine(DrawAutoLine(ray, hit));

            }

        }

        
    }

    IEnumerator DrawAutoLine(Ray ray, RaycastHit hit)
    {

        Vector3 pin;
        for (int i = 1; i < 100; i++)
        {
            //yield return new WaitForSeconds(0.5f);

            
            if (i == 1)
            {
                pin = hit.point;
            } else
            {
                pin = ray.origin;
            }

            ray.origin = pin + (new Vector3(0, 0, ray.origin.z) * Time.deltaTime);
            ray.direction = ray.origin * -1;
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);


            ray.origin += new Vector3(1, 0, 0) * 5 * Time.deltaTime;



            /*if (Physics.SphereCast(ray.origin, 0.5f, ray.direction, out hit, maxDistance))
            {
                ray.origin = hit.point + (new Vector3(0, 0, hit.normal.z + -1) * 5.0f * Time.deltaTime);
                ray.direction = hit.normal * -1;

                //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);

                GameObject point = Instantiate(pointFactory);
                
                
                point.transform.position += new Vector3(1, 0, 0) * 5 * Time.deltaTime;


                ray.origin = hit.point + new Vector3(0, 0, hit.normal.z + -1);
                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            }*/
        }
        yield return new WaitForSeconds(0.5f);
    }


    private LineRenderer createLine()
    {
        GameObject newLine = new GameObject("Line");
        LineRenderer line = newLine.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        line.SetVertexCount(2);
        line.SetWidth(0.02f, 0.02f);
        line.SetColors(Color.black, Color.black);
        line.useWorldSpace = true;

        return line;
    }

    // 파일로더 옵션
    private AssetLoaderOptions GetAssetLoaderOptions()
    {
        var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
        assetLoaderOptions.DontLoadCameras = false;
        assetLoaderOptions.DontLoadLights = false;
        assetLoaderOptions.UseOriginalPositionRotationAndScale = true;
        assetLoaderOptions.DisableAlphaMaterials = true;
        assetLoaderOptions.MaterialShadingMode = MaterialShadingMode.Roughness;
        assetLoaderOptions.AddAssetUnloader = true;
        assetLoaderOptions.GenerateMeshColliders = true;
        assetLoaderOptions.AdvancedConfigs.Add(AssetAdvancedConfig.CreateConfig(AssetAdvancedPropertyClassNames.FBXImportDisableDiffuseFactor, true));
        return assetLoaderOptions;
    }

    public GameObject FindMeshObject(GameObject obj)
    {
        int count = obj.transform.childCount;

        if (count == 0) return null;

        GameObject tObj = null;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            tObj = FindMeshObject(obj.transform.GetChild(i).gameObject);

            if (tObj != null)
            {
                return tObj;
            }
        }

        return null;
    }


}
