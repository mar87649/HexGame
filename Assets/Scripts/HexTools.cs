using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 
using Priority_Queue;


public class HexTools
{
    public static Vector3 rect2Cube(Vector3 hex, float scaleX = 0.433f, float scaleY = 0.75f){
        //Vector3 position = hex.transform.position;

        var col = hex.x / scaleX;
        var row = hex.y / scaleY;

        int x = (int)Math.Round((col - row)/2);
        int z = (int)Math.Round(row);
        int y = -x-z;

        Vector3 results  = new Vector3(x, y, z);

        return results;
    }

    public Vector3 cube2Rect(Vector3 hex, float scaleX = 0.433f, float scaleY = 0.75f, float zOffset = .25f){
        float row =  hex.z;
        float col = (2*hex.x + row);

        float x = col*scaleX;
        float y = row*scaleY;
        float z = zOffset;

        return new Vector3(x, y, z);
    }

    public List<Vector3> neighbors(Vector3 hex, Dictionary<Vector3, GameObject> hexGrid){   
        //hexGrid = GameObject.Find("HexGrid").GetComponent<HexMap>().HexGrid;     
        // Vector3 cords = hex.GetComponent<HexCell>().Cordinates;        
        List<Vector3> results = new List<Vector3>();
        Vector3[] diretions = new [] {
            new Vector3(1, -1, 0),
            new Vector3(1, 0, -1),
            new Vector3(0, 1, -1),
            new Vector3(0, -1, 1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 1, 0)
        };  
        foreach(Vector3 direction in diretions){ 
            Vector3 current = hex + direction; 

            if(reachable(current, hexGrid)){
                results.Add(hex + direction);
            }
            // if(hexGrid.ContainsKey(current))
            // {///and
            //     if(hexGrid[current].GetComponent<HexCell>().Passable)
            //     {
            //         results.Add(hex + direction);
            //     }                
            // }   
        }
        return results;
    }
    public int distance(Vector3 hex1, Vector3 hex2){
        float[] array = {Math.Abs(hex1.x-hex2.x), Math.Abs(hex1.y-hex2.y), Math.Abs(hex1.z-hex2.z)};
        float biggest = array.Max();
        return (int)Math.Round(biggest);
    }
    public Vector3 cubeRound(Vector3 cube){
        float rx = (float)Math.Round(cube.x);
        float ry = (float)Math.Round(cube.y);
        float rz = (float)Math.Round(cube.z);

        float x_diff = Math.Abs(rx - cube.x);
        float y_diff = Math.Abs(ry - cube.y);
        float z_diff = Math.Abs(rz - cube.z);

        if(x_diff > y_diff && x_diff > z_diff){
            rx = -ry-rz;
        }else if(y_diff > z_diff){
            ry = -rx-rz;
        }else{
            rz = -rx-ry;
        }
        return new Vector3(rx, ry, rz);
    }

    public Vector3 cubeLerp(Vector3 a, Vector3 b, float t){
        return new Vector3(
            Mathf.Lerp(a.x, b.x, t),
            Mathf.Lerp(a.y, b.y, t),
            Mathf.Lerp(a.z, b.z, t)
            );
    }

    // // public List<Vector3> line(Vector3 hex1, Vector3 hex2){
    // //     int N = distance(hex1, hex2);
    // //     List<Vector3> results = new List<Vector3>();
    // //     foreach (int i in Enumerable.Range(0, N))
    // //     {
    // //         results.Add(cubeRound(cubeLerp(hex1, hex2, 1/N*i)));
    // //     }

    // //     return results;
    // // }

    // public List<Vector3> line(Vector3 hex1, Vector3 hex2){
    //     int N = distance(hex1, hex2);
    //     List<Vector3> results = new List<Vector3>();
    //     foreach (int i in Enumerable.Range(0, N))
    //     {
    //         results.Add(cubeRound(Vector3.Lerp(hex1, hex2, (1f/N*i))));
    //     }

    //     return results;
    // }

    public List<Vector3> moveRange(Vector3 hex, int N){
        List<Vector3> results = new List<Vector3>();
        for (int x = -N; x <= N; x++)
        {
            for (int y = Math.Max(-N, -x-N); y <= Math.Min(N, -x+N); y++)
            {
                var z = -x-y; 
                results.Add(hex + new Vector3(x, y, z));
            }
        }
        return results;
    } 
    public List<Vector3> pathFind(GameObject startCell, GameObject goalCell, Dictionary<Vector3, GameObject> hexMap)
        {
            Vector3 start = startCell.GetComponent<HexCell>().Cordinates;
            Vector3 goal = goalCell.GetComponent<HexCell>().Cordinates;
            SimplePriorityQueue<Vector3> frontier = new SimplePriorityQueue<Vector3>();
            Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
            Dictionary<Vector3, int> costSoFar = new Dictionary<Vector3, int>();
            List<Vector3> path = new List<Vector3>();
            Vector3 current;

            frontier.Enqueue(start, 0);
            cameFrom.Add(start, default(Vector3));
            costSoFar.Add(start, 0);

            while(frontier.Count > 0){
                current  = frontier.Dequeue();
                if(current.Equals(goal)){
                    break;
                }                
                foreach(Vector3 next in neighbors(current, hexMap)){
                    int newCost = costSoFar[current] + hexMap[current].GetComponent<HexCell>().Cost;
                    if(!costSoFar.ContainsKey(next) || newCost<costSoFar[next]){
                        costSoFar[next] = newCost;
                        int priority = newCost + Heruistic(goal, next);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
            current = goal;
            while(current != start){
                path.Add(current);
                current = cameFrom[current];
            } 
            path.Reverse();
            return path;
        } 
        public int Heruistic(Vector3 p1, Vector3 p2)
        {
            return (int)(Math.Abs(p1.x-p2.x) + Math.Abs(p1.y - p2.y) + Math.Abs(p1.z - p2.z));
        } 
    public bool reachable(Vector3 hex, Dictionary<Vector3, GameObject> hexGrid, bool includeImpassable = false){

        if(hexGrid.ContainsKey(hex))
        {///and
            if(hexGrid[hex].GetComponent<HexCell>().Passable || includeImpassable) 
            {
                return true;
            }                
        } 
        return false; 
    }


}    



