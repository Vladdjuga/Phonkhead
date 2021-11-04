using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Lightning")]
    public Texture2D worldTilesMap;
    public TileMap tileMap;
    //public Material lightShader;
    public float lightTreshold;
    public float lightRadius=7f;
    public Vector2 avarage_block=new Vector2();
    List<Vector2Int> unlitBlocks = new List<Vector2Int>();

    public Character player;
    public CameraController camera;
    public Material material;

    [Header("Sprites")]
    public TilesList tilesList;
    [Header("World attributes")]
    public int world_size = 500;
    public int tree_chance = 10;
    public int world_height = 500;
    public float terrainFreq = 0.04f;
    public float heightMultip = 40f;
    public float heightAdd = 25f;
    public int treeAdd = 10;
    public float stone_height = 25f;
    public int seed = 0;
    [Header("Noise Textures")]
    public float coalSize = 0.06f;
    public float goldSize = 0.06f;
    public float diamondSize = 0.06f;
    public float copperSize = 0.06f;
    public float ironSize = 0.06f;
    public float caveFreq = 0.06f;
    public Texture2D noiseTexture;
    public float coalFreaquency;
    public Texture2D coalTexture;
    public float goldFreaquency;
    public Texture2D goldTexture;
    public float ironFreaquency;
    public Texture2D ironTexture;
    public float diamondFreaquency;
    public Texture2D diamondTexture;
    public float copperFreaquency;
    public Texture2D copperTexture;

    void Start()
    {
        worldTilesMap = new Texture2D(world_size, world_size);
        worldTilesMap.filterMode = FilterMode.Point;

        tileMap = new TileMap(world_size,world_height);
        //lightShader.SetTexture("_Shadow_texture", worldTilesMap);

        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_size; y++)
            {
                worldTilesMap.SetPixel(x, y, Color.white);
            }
        }
        worldTilesMap.Apply();

        noiseTexture = new Texture2D(world_size, world_size);
        coalTexture = new Texture2D(world_size, world_size);
        goldTexture = new Texture2D(world_size, world_size);
        ironTexture = new Texture2D(world_size, world_size);
        diamondTexture = new Texture2D(world_size, world_size);
        copperTexture = new Texture2D(world_size, world_size);

        seed = UnityEngine.Random.Range(-1000, 1000);
        GenerateTexture(caveFreq,0.25f, noiseTexture);
        GenerateTexture(coalFreaquency,coalSize, coalTexture);
        GenerateTexture(goldFreaquency,goldSize, goldTexture);
        GenerateTexture(ironFreaquency,ironSize, ironTexture);
        GenerateTexture(diamondFreaquency,diamondSize, diamondTexture);
        GenerateTexture(copperFreaquency,copperSize, copperTexture);

        GenerateTerrain();

        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_size; y++)
            {
                if (worldTilesMap.GetPixel(x, y) == Color.white)
                    LightBlock(x, y, 1f, 0);
            }
        }
        float yt = 0;
        Array copyArray = new TileObject[tileMap.tiles[(int)(world_size / 2)].Length];
        tileMap.tiles[(int)(world_size / 2)].CopyTo(copyArray, 0);
        Array.Reverse(copyArray);
        foreach (TileObject item in copyArray)
        {
            if (item != null)
            {
                yt = item.position.y;
                break;
            }
        }
        //avarage_block = tileMap.tiles[(int)(world_size / 2)][tileMap.tiles[(int)(world_size / 2)].Length].position;
        player.transform.position = new Vector3((int)(world_size / 2), yt+3f, player.transform.position.z);
    }
    void LightBlock(int x,int y,float intensity,int iteration)
    {
        if (iteration < lightRadius)
        {
            worldTilesMap.SetPixel(x, y, Color.white*intensity);

            for (int nx = x-1; nx < x+2; nx++)
            {
                for (int ny = y-1; ny < y+2; ny++)
                {
                    if (nx != x || ny != y)
                    {
                        float dist = Vector2.Distance(new Vector2(x, y), new Vector2(nx, ny));
                        float targetIntensity = Mathf.Pow(0.7f, dist)*intensity;
                        if (worldTilesMap.GetPixel(nx, ny) != null)
                        {
                            if (worldTilesMap.GetPixel(nx, ny).r < targetIntensity)
                            {
                                LightBlock(nx, ny, targetIntensity, iteration + 1);
                            }
                        }
                    }
                }
            }
            worldTilesMap.Apply();
        }
    }
    void RemoveLightSource(int x,int y)
    {
        unlitBlocks.Clear();
        UnLightBlock(x, y, x, y);

        List<Vector2Int> toRelight = new List<Vector2Int>();
        foreach (var block in unlitBlocks)
        {
            for (int nx = block.x-1; nx < block.x+2; nx++)
            {
                for (int ny = block.y - 1; ny < block.y + 2; ny++)
                {
                    if (worldTilesMap.GetPixel(nx, ny) != null)
                    {
                        if (worldTilesMap.GetPixel(nx, ny).r > worldTilesMap.GetPixel(block.x, block.y).r)
                        {
                            if (!toRelight.Contains(new Vector2Int(nx, ny)))
                                toRelight.Add(new Vector2Int(nx, ny));
                        }
                    }
                }
            }
        }
        foreach (var source in toRelight)
        {
            LightBlock(source.x, source.y, worldTilesMap.GetPixel(source.x,source.y).r,0);
        }
        worldTilesMap.Apply();
    }
    void UnLightBlock(int x,int y,int ix,int iy)
    {
        if(Mathf.Abs(x-ix)>=lightRadius||Mathf.Abs(y-iy)>=lightRadius||unlitBlocks.Contains(new Vector2Int(x, y)))
        {
            return;
        }
        for (int nx = x - 1; nx < x + 2; nx++)
        {
            for (int ny = y - 1; ny < y + 2; ny++)
            {
                if (nx != x || ny != y)
                {
                    if (worldTilesMap.GetPixel(nx, ny) != null)
                    {
                        if (worldTilesMap.GetPixel(nx, ny).r < worldTilesMap.GetPixel(x, y).r)
                        {
                            UnLightBlock(nx, ny, ix, iy);
                        }
                    }
                }
            }
        }
        worldTilesMap.SetPixel(x, y, Color.black);
        unlitBlocks.Add(new Vector2Int(x, y));
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && targetObject.transform.gameObject.tag == "Floor")
            {
                GameObject go = targetObject.transform.gameObject;
                Destroy(go);
                CreateDrop(go.GetComponent<SpriteRenderer>().sprite, go.transform.position.x, go.transform.position.y, "Drop", go.transform.parent);
                worldTilesMap.SetPixel((int)go.transform.position.x, (int)go.transform.position.y, Color.white);
                //LightBlock((int)go.transform.position.x, (int)go.transform.position.y,1f,0);
                tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y] = null;
            }
            worldTilesMap.Apply();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            //if (targetObject && targetObject.transform.gameObject.tag == "Floor")
            //{
            //    GameObject go = targetObject.transform.gameObject;
            //    Destroy(go);
            //    CreateTileFront(tilesList.earth.sprite, go.transform.position.x, go.transform.position.y, "Floor");
            //}
            if (targetObject&&targetObject.name=="sky")
            {
                GameObject go = targetObject.transform.gameObject;
          
                if (tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y]==null)
                {
                    CreateTileFrontTriggerLight(tilesList.torch_prefab, go.transform.position.x, go.transform.position.y, "Lava", go.transform.parent);
                }
                //CreateTileBg(tilesList.earth.sprite, go.transform.position.x, go.transform.position.y, "Floor");
            }
        }
    }
    public void GenerateTerrain()
    {
        int chunks_count = world_size / 16;
        int x_chunk = 16;
        GameObject parent_of_parents = new GameObject();
        parent_of_parents.name = "parent of parents";
        parent_of_parents.transform.position = new Vector3(0.5f, 0.5f, 999);
        parent_of_parents.transform.parent = this.transform;
        Transform parent = parent_of_parents.transform;
        for (int x = 0; x < world_size; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultip + heightAdd;
            if (x_chunk >= 16)
            {
                x_chunk = 0;
                parent = Instantiate(new GameObject(), new Vector3(x + 0.5f, 0.5f, 999), Quaternion.identity, parent_of_parents.transform).transform;
            }
            x_chunk++;
            for (int y = 0; y < height; y++)
            {
                if (!(x == 0 || x == world_size - 1 || y == 0 || y == world_height - 1))
                {
                    if (y < 3)
                    {
                        CreateTileFront(tilesList.earth.sprite, x + 0.5f, y + 0.5f, "Floor", parent.transform);
                        //CreateTileBg(tilesList.earth.sprite, x + 0.5f, y + 0.5f, "Floor_bg");
                    }

                    else if (noiseTexture.GetPixel(x, y).r > 0.25f)
                    {
                        Sprite sprite = tilesList.stone.sprite;
                        if (y < height - stone_height)
                        {
                            int rand = UnityEngine.Random.Range(0, 2);
                            if(rand==0)
                                sprite = tilesList.stone.sprite;
                            else
                                sprite = tilesList.gravel.sprite;
                            if (coalTexture.GetPixel(x, y).r > coalFreaquency)
                            {
                                sprite = tilesList.coal.sprite;
                            }
                            else if (goldTexture.GetPixel(x, y).r > goldFreaquency)
                            {
                                sprite = tilesList.gold.sprite;
                            }
                            else if(copperTexture.GetPixel(x, y).r > copperFreaquency)
                            {
                                sprite = tilesList.copper.sprite;
                            }
                            else if(diamondTexture.GetPixel(x, y).r > diamondFreaquency)
                            {
                                sprite = tilesList.diamond.sprite;
                            }
                            else if(ironTexture.GetPixel(x, y).r > ironFreaquency)
                            {
                                sprite = tilesList.iron.sprite;
                            }
                        }
                        else if (y < height - 1)
                            sprite = tilesList.earth.sprite;
                        else
                        {
                            sprite = tilesList.grass.sprite;
                            if (Mathf.RoundToInt(x) == Mathf.RoundToInt(world_size / 2))
                            {
                                avarage_block = new Vector2(x, y + 3f);
                            }

                            int rand = UnityEngine.Random.Range(0, tree_chance);
                            int rand2 = UnityEngine.Random.Range(0, tilesList.tiny_sprites.Length);
                            if (rand == 1)
                            {
                                GenerateTree(tilesList.log.sprite, x + 0.5f, y + 0.5f, "Floor", parent.transform);
                            }
                            else if(rand>2&&rand<7)
                            {
                                CreateTileFrontTrigger(tilesList.tiny_sprites[rand2].sprite, x + 0.5f, y + 0.5f + 1f, "Tiny", parent.transform);
                            }
                        }
                        CreateTileFront(sprite, x+0.5f, y + 0.5f, "Floor", parent.transform);
                        //CreateTileBg(sprite, x + 0.5f, y + 0.5f, "Floor_bg");
                    }
                    //else if (y < 40)
                    //{
                    //    GameObject newTile = new GameObject(name = "water");
                    //    newTile.transform.parent = this.transform;
                    //    newTile.AddComponent<SpriteRenderer>();
                    //    newTile.GetComponent<SpriteRenderer>().sprite = water;
                    //    newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, -5);
                    //    newTile.AddComponent<BoxCollider2D>();
                    //    newTile.GetComponent<BoxCollider2D>().isTrigger = true;

                    //    var renderer = newTile.GetComponent<Renderer>();
                    //    renderer.material.shader = Shader.Find("Transparent/Diffuse");
                    //    renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.9f);
                    //    newTile.tag = "Water";
                    //}
                    //else
                    //{
                    //    GameObject newTile = new GameObject(name = "sky");
                    //    newTile.transform.parent = this.transform;
                    //    newTile.AddComponent<SpriteRenderer>();
                    //    newTile.GetComponent<SpriteRenderer>().sprite = tilesList.air.sprite;
                    //    newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 5);
                    //}
                }
            }
        }
        GameObject air_bedrock = new GameObject();
        air_bedrock.name = "air bedrock";
        air_bedrock.transform.parent = parent_of_parents.transform;
        air_bedrock.transform.position = parent_of_parents.transform.position;
        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_height; y++)
            {
                if (x == 0 || x == world_size - 1 || y == 0 || y == world_height - 1)
                {
                    GameObject newTile = new GameObject(name = "bedrock");
                    newTile.transform.parent = air_bedrock.transform;
                    newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 30);
                    newTile.AddComponent<BoxCollider2D>();
                }
                else
                {
                    if (y > 80)
                    {
                        GameObject newTile = new GameObject(name = "sky");
                        newTile.transform.parent = air_bedrock.transform;
                        newTile.AddComponent<SpriteRenderer>();
                        newTile.GetComponent<SpriteRenderer>().sprite = tilesList.air_top.sprite;
                        newTile.GetComponent<SpriteRenderer>().material = material;
                        newTile.AddComponent<BoxCollider2D>().isTrigger=true;
                        newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 15);
                    }
                    else if (y == 80)
                    {
                        int rand = UnityEngine.Random.Range(0, 3);
                        Sprite skybox = tilesList.air_side.sprite;
                        switch (rand)
                        {
                            case 1:
                                skybox = tilesList.air_sideClouds.sprite;
                                break;
                            case 2:
                                skybox = tilesList.air_sideHills.sprite;
                                break;
                        }
                        GameObject newTile = new GameObject(name = "sky");
                        newTile.transform.parent = air_bedrock.transform;
                        newTile.AddComponent<SpriteRenderer>();
                        newTile.GetComponent<SpriteRenderer>().sprite = skybox;
                        newTile.GetComponent<SpriteRenderer>().material = material;
                        newTile.AddComponent<BoxCollider2D>().isTrigger = true;
                        newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 15);
                    }
                    else
                    {
                        GameObject newTile = new GameObject(name = "sky");
                        newTile.transform.parent = air_bedrock.transform;
                        newTile.AddComponent<SpriteRenderer>();
                        newTile.GetComponent<SpriteRenderer>().sprite = tilesList.air.sprite;
                        newTile.GetComponent<SpriteRenderer>().material = material;
                        newTile.AddComponent<BoxCollider2D>().isTrigger = true;
                        newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 15);
                    }
                }
            }
        }
        worldTilesMap.Apply();
    }
    public void GenerateTree(Sprite log_1, float x, float y, string tag, Transform parent)
    {
        int rand = UnityEngine.Random.Range(3, treeAdd);
        for (int i = 1; i <= rand; i++)
        {
            CreateTileFrontTrigger(log_1, x, y+i, tag, parent);
        }
        int h = UnityEngine.Random.Range(3, 6);
        for (int i = 1; i <= h; i++)
        {
            CreateTileFrontTrigger(tilesList.leaf.sprite, x, y + i + rand, tag, parent);
            int w = UnityEngine.Random.Range(1, 3);
            for (int j = 1; j <= w; j++)
            {
                CreateTileFrontTrigger(tilesList.leaf.sprite, x + j, y + i+ rand, tag, parent);
            }
            CreateTileFrontTrigger(tilesList.leaf.sprite, x, y + i + rand, tag, parent);
            w = UnityEngine.Random.Range(1, 3);
            for (int j = 1; j <= w; j++)
            {
                CreateTileFrontTrigger(tilesList.leaf.sprite, x - j, y + i + rand, tag, parent);
            }
        }
    }
    public void CreateTileFrontTrigger(Sprite sprite, float x, float y, string tag, Transform parent)
    {
        GameObject newTile = new GameObject(name = sprite.name);
        newTile.transform.parent = parent;
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile.GetComponent<SpriteRenderer>().material = material;
        newTile.transform.position = new Vector3(x, y, 5);
        newTile.AddComponent<BoxCollider2D>();
        newTile.GetComponent<BoxCollider2D>().isTrigger=true;
        newTile.tag = tag;
    }
    public void CreateTileFrontTriggerLight(GameObject sprite, float x, float y, string tag, Transform parent)
    {
        GameObject newTile = Instantiate(sprite, new Vector3(x, y, 5),Quaternion.identity, parent);
        tileMap.addTile(sprite.name, true, false, 5, newTile.transform.position, (int)x, (int)y);
    }
    public void CreateDrop(Sprite sprite, float x, float y, string tag, Transform parent)
    {
        GameObject newTile = new GameObject(name = sprite.name);
        newTile.transform.parent = parent;
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile.GetComponent<SpriteRenderer>().material = material;
        newTile.transform.position = new Vector3(x, y, -5);
        newTile.AddComponent<BoxCollider2D>();
        newTile.GetComponent<BoxCollider2D>().usedByEffector = true;
        newTile.transform.localScale = new Vector2(0.5f,0.5f);
        newTile.AddComponent<Rigidbody2D>();
        newTile.GetComponent<Rigidbody2D>().gravityScale=15;
        newTile.AddComponent<PlatformEffector2D>();
        newTile.GetComponent<PlatformEffector2D>().useOneWay = false;
        newTile.tag = tag;
    }
    public void CreateTileFront(Sprite sprite , float x, float y,string tag, Transform parent)
    {
        GameObject newTile = new GameObject(name = sprite.name);
        newTile.transform.parent = parent;
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile.GetComponent<SpriteRenderer>().material = material;
        newTile.transform.position = new Vector3(x, y, -5);
        newTile.AddComponent<BoxCollider2D>();
        newTile.GetComponent<BoxCollider2D>().usedByEffector=true;
        newTile.GetComponent<BoxCollider2D>().size=Vector2.one;
        newTile.AddComponent<PlatformEffector2D>();
        newTile.GetComponent<PlatformEffector2D>().useOneWay=false;
        worldTilesMap.SetPixel((int)x, (int)y,Color.black);
        tileMap.addTile(sprite.name, true, false, 5, newTile.transform.position, (int)x, (int)y);
        //RemoveLightSource((int)x, (int)y);
        newTile.tag = tag;
        //newTile.layer = 3;
    }
    public void CreateTileBg(Sprite sprite, float x, float y, string tag, Transform parent)
    {
        GameObject newTile2 = new GameObject(name = sprite.name);
        newTile2.transform.parent = parent;
        newTile2.AddComponent<SpriteRenderer>();
        newTile2.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile2.GetComponent<SpriteRenderer>().material = material;
        newTile2.transform.position = new Vector3(x, y, 5);
        //
        Color oldColor = newTile2.GetComponent<SpriteRenderer>().color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.8f);
        newTile2.GetComponent<SpriteRenderer>().color = newColor;
        newTile2.AddComponent<BoxCollider2D>();
        newTile2.GetComponent<BoxCollider2D>().isTrigger = true;
        newTile2.tag = tag;
    }
    public void GenerateTexture(float frequency,float limit,Texture2D texture)
    {
        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_size; y++)
            {
                float v = Mathf.PerlinNoise((seed + x) * frequency, (seed + y) * frequency);
                if(v>limit)
                    texture.SetPixel(x, y, Color.white);
                else
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
    }
}
