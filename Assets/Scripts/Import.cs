using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriLib;

public class Import : MonoBehaviour
{
    GameObject meshObj = null;

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
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.position = new Vector3(0, 0, 10);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
