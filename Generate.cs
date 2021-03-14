using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make your generation algorithm here
public class Generate : MonoBehaviour {

    public TileType[] tileTypes; // all the tiletypes given in the editor
    public GameObject player;
    public GameObject enemy;
    
    //the size of the generated map
    private int mapSizeX = 10;
    private int mapSizeY = 10;
    

    void Start()
    {
        GenerateMap();
    }
    
    /*
     * Generates a map
     * Currently just a green field with a player and an enemy
     */ 
    void GenerateMap()
    {
        
        //Set random X coordinate for start tile and player tile, but force the start Y coordinate to be 0
        //so that player and start tile are always at the bottom of the map.
        var startPosX = Random.Range(0, 10);
        var startPosY = 0;

        //Same as the start tile and player but for the end tile and at the opposite side of the map
        var endPosX = Random.Range(0, 10);
        var endPosY = 9;


        //Generate grass tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[0];
                GameObject go =Instantiate(tt.tileVisual, new Vector3(x, y,0), Quaternion.identity);
            }
        }

        //Set the tiles 1 = Wall , 2 = Start tile, 3 = End tile
        TileType ww = tileTypes[1];
        TileType pp = tileTypes[2];
        TileType ee = tileTypes[3];
        GameObject newPlayer = Instantiate(player, new Vector3(startPosX, startPosY, -1.5f),Quaternion.identity); //spawn one player
        
        GameObject newStart = Instantiate(pp.tileVisual, new Vector3(startPosX, startPosY, -0.5f), Quaternion.identity); //spawn the start tile
        GameObject newEnd = Instantiate(ee.tileVisual, new Vector3(endPosX, endPosY, -0.5f), Quaternion.identity);//spawn the end tile

        //Array for walls.
        GameObject[] walls = new GameObject[50];
        int counter = 0;
        //Fill half of the map with walls
        for (int i = 0; i < mapSizeX/2; i++)
        {
            for(int j = 0; j < mapSizeY/2; j++)
            {
                //Give the walls random coordinates
                var randX = Random.Range(0, 10);
                var randY = Random.Range(0, 10);

                //Check variable is used to make sure that walls wont go top of player/enemy or start/end tile.
                var check = new Vector3(randX, randY, -0.5f);

                //Check if coordinate is same as player/enemy or end/start tile
                //if not then place the new wall.
                if (check != newPlayer.transform.localPosition && check != newStart.transform.localPosition && check != newEnd.transform.localPosition)
                {
                    GameObject newTile = Instantiate(ww.tileVisual, new Vector3(randX, randY, -0.5f), Quaternion.identity);
                    //Add wall to the array
                    walls[counter] = newTile;
                    counter++;
                }
                else 
                {
                    j--;
                    i--;
                };


            }

        }


        //I tried to check if there is a wall where the enemy is going to be but I run out of time
        //as of now it will place the enemy behind the wall but some times if there
        //are two walls in a column then the enemy may get inside the wall
        //otherwise the enemy is spawned correctly
        for (int z = 0; z < walls.Length; z++)
        {
            //Get wall x and y coordinates from array
            var wallX = walls[z].transform.localPosition.x;
            var wallY = walls[z].transform.localPosition.y;

            if (1 < wallY && wallY < 8)
            {
                for(int k = 0; k < walls.Length; k++)
                {
                    var checkWallY = walls[k].transform.localPosition.y;
                    //Check all of the walls Y coordinate and if there is a wall in a way break from loop
                    if ((wallY + 1) == checkWallY)
                    {
                        break;
                    }
                    else 
                    {
                        GameObject newEnemy = Instantiate(enemy, new Vector3(wallX, wallY + 1, -1.5f), Quaternion.identity); //and one enemy and break from the loop
                        break;
                    } 
                }
                break;
            }
           
           
        }
       
    }
    
}
