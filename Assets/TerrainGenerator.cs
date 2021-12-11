using Assets.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public TileMap tileMap;

    public Character player;
    public CameraController camera;
    public InventoryScript inventory;
    public Material material;
    //[Header("Player PUN")]
    //public PlayerTransform playerTransform;

    [Header("Biomes")]
    public float biomesFreq = 0.04f;
    public Gradient biomesGradient;
    public Texture2D biomesMap;
    public BiomeScript[] biomes;
    public Color[] biomesColors;

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
    //[Header("Noise Textures")]
    //public float coalSize = 0.06f;
    //public float goldSize = 0.06f;
    //public float diamondSize = 0.06f;
    //public float copperSize = 0.06f;
    //public float ironSize = 0.06f;
    //public float caveFreq = 0.06f;
    //public Texture2D noiseTexture;
    //public float coalFreaquency;
    //public Texture2D coalTexture;
    //public float goldFreaquency;
    //public Texture2D goldTexture;
    //public float ironFreaquency;
    //public Texture2D ironTexture;
    //public float diamondFreaquency;
    //public Texture2D diamondTexture;
    //public float copperFreaquency;
    //public Texture2D copperTexture;

    void Start()
    {
        float yt = 0;
            tileMap = new TileMap(world_size, world_height);


            GenerateTerrain();
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
        player.transform.position = new Vector3((int)(world_size / 2), yt + 3f, player.transform.position.z);
        string rand_world = this.GetHashCode().ToString();
        SavingWorldHelper.Save(tileMap,"world_"+rand_world);
    }
    void OnValidate()
    {
        biomesColors = new Color[biomes.Length];
        for (int i = 0; i < biomes.Length; i++)
        {
            biomesColors[i] = biomes[i].biomeColor;
        }
        GenerateTextures();
    }

    private void GenerateTextures()
    {
        //noiseTexture = new Texture2D(world_size, world_size);
        biomesMap = new Texture2D(world_size, world_size);
        //coalTexture = new Texture2D(world_size, world_size);
        //goldTexture = new Texture2D(world_size, world_size);
        //ironTexture = new Texture2D(world_size, world_size);
        //diamondTexture = new Texture2D(world_size, world_size);
        //copperTexture = new Texture2D(world_size, world_size);

        seed = UnityEngine.Random.Range(-1000, 1000);
        DrawBiomesTexture();
        for (int i = 0; i < biomes.Length; i++)
        {
            biomes[i].noiseTexture = new Texture2D(world_size, world_size);
            biomes[i].coalTexture = new Texture2D(world_size, world_size);
            biomes[i].goldTexture = new Texture2D(world_size, world_size);
            biomes[i].ironTexture = new Texture2D(world_size, world_size);
            biomes[i].diamondTexture = new Texture2D(world_size, world_size);
            biomes[i].copperTexture = new Texture2D(world_size, world_size);

            GenerateTexture(biomes[i].caveFreq, 0.25f, biomes[i].noiseTexture);
            GenerateTexture(biomes[i].coalFreaquency, biomes[i].coalSize, biomes[i].coalTexture);
            GenerateTexture(biomes[i].goldFreaquency, biomes[i].goldSize, biomes[i].goldTexture);
            GenerateTexture(biomes[i].ironFreaquency, biomes[i].ironSize, biomes[i].ironTexture);
            GenerateTexture(biomes[i].diamondFreaquency, biomes[i].diamondSize, biomes[i].diamondTexture);
            GenerateTexture(biomes[i].copperFreaquency, biomes[i].copperSize, biomes[i].copperTexture);

        }
    }
    void DrawBiomesTexture()
    {
        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_size; y++)
            {
                float v = Mathf.PerlinNoise((seed + x) * biomesFreq, (seed+y) * biomesFreq);
                var biomeColor = biomesGradient.Evaluate(v);
                biomesMap.SetPixel(x, y, biomeColor);
            }
        }
        biomesMap.Apply();
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y]!=null && targetObject.name != "sky")
            {

                int module = (int)Mathf.Sqrt((float)(Math.Pow(mousePosition.x - player.transform.position.x, 2) + Math.Pow(mousePosition.y - player.transform.position.y, 2)));
                if (module < 5)
                {
                    if (tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y].is_breakable)
                    {
                        GameObject go = targetObject.transform.gameObject;
                        Destroy(go);
                        CreateDrop(go.GetComponent<SpriteRenderer>().sprite, go.transform.position.x, go.transform.position.y, "Drop", go.transform.parent);
                        tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y] = null;
                    }
                }
            }
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
            if (targetObject && targetObject.name == "sky")
            {
                GameObject go = targetObject.transform.gameObject;

                if (tileMap.tiles[(int)mousePosition.x][(int)mousePosition.y] == null)
                {
                    try
                    {
                        string block_name = inventory.inventory.items[inventory.selected].name;
                        TileDef tile = tilesList.GetTileByName(block_name);
                        inventory.inventory.RemoveSingleItemAt(inventory.selected);
                        //CreateTileFrontTriggerLight(tilesList.torch_prefab, go.transform.position.x, go.transform.position.y, "Lava", go.transform.parent);
                        CreateTileFront(tile.sprite, go.transform.position.x, go.transform.position.y, "Floor", go.transform.parent, tile.name);
                    }
                    catch(Exception ex) { }
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
                Color ind = biomesMap.GetPixel(x, y);
                BiomeScript currentBiome = biomes[Array.IndexOf(biomesColors, ind)];

                if (y < 3)
                {
                    CreateTileFront(currentBiome.tilesList.earth.sprite, x + 0.5f, y + 0.5f, "Floor", parent.transform, currentBiome.tilesList.earth.name);
                    //CreateTileBg(tilesList.earth.sprite, x + 0.5f, y + 0.5f, "Floor_bg");
                }

                else if (currentBiome.noiseTexture.GetPixel(x, y).r > 0.25f)
                {
                    Sprite sprite = currentBiome.tilesList.stone.sprite;
                    string name_ = currentBiome.tilesList.stone.name;
                    if (y < height - stone_height)
                    {
                        int rand = UnityEngine.Random.Range(0, 2);
                        if (rand == 0)
                        {
                            sprite = currentBiome.tilesList.stone.sprite;
                            name_ = currentBiome.tilesList.stone.name;
                        }
                        else
                        {
                            sprite = currentBiome.tilesList.gravel.sprite;
                            name_ = currentBiome.tilesList.gravel.name;
                        }
                        if (currentBiome.coalTexture.GetPixel(x, y).r > currentBiome.coalFreaquency)
                        {
                            sprite = currentBiome.tilesList.coal.sprite;
                            name_ = currentBiome.tilesList.coal.name;
                        }
                        else if (currentBiome.goldTexture.GetPixel(x, y).r > currentBiome.goldFreaquency)
                        {
                            sprite = currentBiome.tilesList.gold.sprite;
                            name_ = currentBiome.tilesList.gold.name;
                        }
                        else if (currentBiome.copperTexture.GetPixel(x, y).r > currentBiome.copperFreaquency)
                        {
                            sprite = currentBiome.tilesList.copper.sprite;
                            name_ = currentBiome.tilesList.copper.name;
                        }
                        else if (currentBiome.diamondTexture.GetPixel(x, y).r > currentBiome.diamondFreaquency)
                        {
                            sprite = currentBiome.tilesList.diamond.sprite;
                            name_ = currentBiome.tilesList.diamond.name;
                        }
                        else if (currentBiome.ironTexture.GetPixel(x, y).r > currentBiome.ironFreaquency)
                        {
                            sprite = currentBiome.tilesList.iron.sprite;
                            name_ = currentBiome.tilesList.iron.name;
                        }
                    }
                    else if (y < height - 1)
                    {
                        sprite = currentBiome.tilesList.earth.sprite;
                        name_ = currentBiome.tilesList.earth.name;
                    }
                    else
                    {
                        sprite = currentBiome.tilesList.grass.sprite;
                        name_ = currentBiome.tilesList.grass.name;

                        int rand = UnityEngine.Random.Range(0, currentBiome.tree_chance);
                        int rand2 = UnityEngine.Random.Range(0, currentBiome.tilesList.tiny_sprites.Length);
                        if (rand == 1)
                        {
                            GenerateTree(currentBiome.tilesList.log.sprite, x + 0.5f, y + 0.5f, "Floor", parent.transform, currentBiome);
                        }
                        else if (rand > 2 && rand < 7)
                        {
                            CreateTileFrontTrigger(currentBiome.tilesList.tiny_sprites[rand2].sprite, x + 0.5f, y + 0.5f + 1f, "Tiny", parent.transform);
                        }
                    }
                    CreateTileFront(sprite, x + 0.5f, y + 0.5f, "Floor", parent.transform, name_);
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
        GameObject air_bedrock = new GameObject();
        air_bedrock.name = "air bedrock";
        air_bedrock.transform.parent = parent_of_parents.transform;
        air_bedrock.transform.position = parent_of_parents.transform.position;
        for (int x = -1; x <= world_size; x++)
        {
            for (int y = -1; y <= world_height; y++)
            {
                if (x == -1 || x == world_size || y == -1 || y == world_height)
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
                        newTile.AddComponent<BoxCollider2D>().isTrigger = true;
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
    }
    public void GenerateTree(Sprite log_1, float x, float y, string tag, Transform parent, BiomeScript currentBiome)
    {
        int rand = UnityEngine.Random.Range(3, currentBiome.treeAdd);
        for (int i = 1; i <= rand; i++)
        {
            CreateTileFrontTrigger(log_1, x, y + i, tag, parent);
        }
        int h = UnityEngine.Random.Range(3, 6);
        for (int i = 1; i <= h; i++)
        {
            CreateTileFrontTrigger(currentBiome.tilesList.leaf.sprite, x, y + i + rand, tag, parent);
            int w = UnityEngine.Random.Range(1, 3);
            if (w + x < world_size)
            {
                for (int j = 1; j <= w; j++)
                {
                    CreateTileFrontTrigger(currentBiome.tilesList.leaf.sprite, x + j, y + i + rand, tag, parent);
                }
            }
            w = UnityEngine.Random.Range(1, 3);
            if (x-w > 0)
            {
                for (int j = 1; j <= w; j++)
                {
                    CreateTileFrontTrigger(currentBiome.tilesList.leaf.sprite, x - j, y + i + rand, tag, parent);
                }
            }
        }
    }
    public void CreateTileFrontTrigger(Sprite sprite, float x, float y, string tag, Transform parent)
    {
        if (tileMap.tiles[(int)x][(int)y] == null)
        {
            GameObject newTile = new GameObject(name = sprite.name);
            newTile.transform.parent = parent;
            newTile.AddComponent<SpriteRenderer>();
            newTile.GetComponent<SpriteRenderer>().sprite = sprite;
            newTile.GetComponent<SpriteRenderer>().material = material;
            newTile.transform.position = new Vector3(x, y, 5);
            newTile.AddComponent<BoxCollider2D>();
            newTile.GetComponent<BoxCollider2D>().isTrigger = true;
            tileMap.addTile(sprite.name, true, false, 5, newTile.transform.position, (int)x, (int)y);
            newTile.tag = tag;
        }
    }
    public void CreateTileFrontTriggerLight(GameObject sprite, float x, float y, string tag, Transform parent)
    {
        if (tileMap.tiles[(int)x][(int)y] == null)
        {
            GameObject newTile = Instantiate(sprite, new Vector3(x, y, 5), Quaternion.identity,parent);
            tileMap.addTile(sprite.name, true, false, 5, newTile.transform.position, (int)x, (int)y);
        }
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
        newTile.transform.localScale = new Vector2(0.5f, 0.5f);
        newTile.AddComponent<Rigidbody2D>();
        newTile.GetComponent<Rigidbody2D>().gravityScale = 15;
        newTile.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        newTile.AddComponent<PlatformEffector2D>();
        newTile.GetComponent<PlatformEffector2D>().useOneWay = false;
        newTile.tag = tag;
    }
    public void CreateTileFront(Sprite sprite, float x, float y, string tag, Transform parent,string name_)
    {
        if (tileMap.tiles[(int)x][(int)y] == null)
        {
            GameObject newTile = new GameObject(name = name_);
        newTile.transform.parent = parent;
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile.GetComponent<SpriteRenderer>().material = material;
        newTile.transform.position = new Vector3(x, y, -5);
        newTile.AddComponent<BoxCollider2D>();
        newTile.GetComponent<BoxCollider2D>().usedByEffector = true;
        newTile.GetComponent<BoxCollider2D>().size = Vector2.one;
        newTile.AddComponent<PlatformEffector2D>();
        newTile.GetComponent<PlatformEffector2D>().useOneWay = false;
        tileMap.addTile(name_, true, false, 5, newTile.transform.position, (int)x, (int)y);
        //RemoveLightSource((int)x, (int)y);
        newTile.tag = tag;
        //newTile.layer = 3;
        }
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
    public void GenerateTexture(float frequency, float limit, Texture2D texture)
    {
        for (int x = 0; x < world_size; x++)
        {
            for (int y = 0; y < world_size; y++)
            {
                float v = Mathf.PerlinNoise((seed + x) * frequency, (seed + y) * frequency);
                if (v > limit)
                    texture.SetPixel(x, y, Color.white);
                else
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
    }
}
