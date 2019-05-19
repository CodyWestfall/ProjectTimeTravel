using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGeneration: MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    [Range(1,20)]
    public int minHeightLevelLenght, heightLevelRandomRange;

    public int startHeight,coinAmount;

    private static int _heightLevelRandomRange, _startHeight;

    public GameObject coinPrefab;

    private GameObject environment;

    void Start()
    {
        _startHeight = startHeight;
        _heightLevelRandomRange = heightLevelRandomRange;

        environment = GameObject.FindGameObjectWithTag("Environment");

        //Creating a plane map with the given height/lenght
        int[,] array = GenerateArray(200, 100, true);

        //Genereation random seed
        float random = Random.Range(0f, 20f);

        //Convert the plane map to a random smoothed map
        array = RandomMapSmooth(array,random,minHeightLevelLenght);

        //Render the map so it is displayed in the game
        RenderMap(array, tilemap, tile);

        AddCoins(array, coinAmount, coinPrefab, environment);
    }



    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        //Clear the map
        tilemap.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

    public static int[,] RandomMapSmooth(int[,] map, float seed, int minSectionWidth)
    {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Determine the start position
        int lastHeight = _startHeight;
       
        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            //Used to make different lenghts of height levels
            int randomSectionWidth = Random.Range(minSectionWidth, _heightLevelRandomRange);

            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minium(random) required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > randomSectionWidth)
            {
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > randomSectionWidth)
            {
                lastHeight++;
                sectionWidth = 0;
            }
            //add to the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }

        //Return the map
        return map;
    }

    public static void AddCoins(int[,]map, int maxCoinAmount,GameObject coinObject, GameObject parentObject)
    {
        bool last = false;
        List<Vector3> coinPosList = new List<Vector3>();

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {

            for(int y= 0; y < map.GetUpperBound(1); y++)
            {
                if(map[x,y] == 0)
                {
                    if(last == true)
                    {
                        Vector3 coinPos = new Vector3(x+0.5f, y + 0.5f, 0);                       
                        coinPosList.Add(coinPos);
                    }
                    last = false;
                }
                else
                {
                    last = true;                      
                }
            }
        }

        for(int coin = 0; coin < maxCoinAmount; coin++)
        {
            int pos = Random.Range(1, coinPosList.Count);
            var cloneCoin = Instantiate(coinObject, coinPosList[pos],Quaternion.identity);
            cloneCoin.transform.parent = parentObject.transform;
            try
            {
                coinPosList.RemoveRange(pos - 1, 3);
            }
            catch
            {
                coinPosList.RemoveAt(pos);
            }
            
        }           
    }
}
