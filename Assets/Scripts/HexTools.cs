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
    public bool reachableHex(Vector3 hex, Dictionary<Vector3, GameObject> hexGrid, bool includeImpassable = false){

        if(hexGrid.ContainsKey(hex))
        {///and
            if(hexGrid[hex].GetComponent<HexCell>().Passable || includeImpassable) 
            {
                return true;
            }                
        } 
        return false; 
    }
    public List<Vector3> neighbors(Vector3 hex, Dictionary<Vector3, GameObject> hexGrid){   
   
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

            //if(reachableHex(current, hexGrid, includeImpassable)){
            if(hexGrid.ContainsKey(hex)){
                results.Add(hex + direction);
            }  
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
    public List<Vector3> hexRange(Vector3 hex, int N){
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
    public List<Vector3> pathFind(GameObject unit, GameObject goalHex, Dictionary<Vector3, GameObject> hexMap)
        {
            Vector3 start = unit.GetComponent<UnitProps>().CordPosition;
            Vector3 goal = goalHex.GetComponent<HexCell>().Cordinates;
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
                    //check if neighbor contain a unit
                    //if true, check if units are enemies
                    //if true, skip neighbor, else continue
                    if(hexMap[next].GetComponent<HexCell>().Unit != null){
                        if(areEnemies(unit, hexMap[next].GetComponent<HexCell>().Unit)){
                            continue;
                        }
                    }                    

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
            path.Add(start);
            path.Reverse();
            return path;
        } 
    public int Heruistic(Vector3 p1, Vector3 p2)
        {
            return (int)(Math.Abs(p1.x-p2.x) + Math.Abs(p1.y - p2.y) + Math.Abs(p1.z - p2.z));
        } 
    public List<Vector3> searchRange(GameObject Object, int range, Dictionary<Vector3, GameObject> hexMap){
       

        Vector3 start = rect2Cube(Object.transform.position);
        List<Vector3> visited = new List<Vector3>();
        List<List<Vector3>> fringes = new List<List<Vector3>>();
        visited.Add(start);
        fringes.Add(new List<Vector3>(){start});

        for (int k = 1; k <= range; k++){
            fringes.Add(new List<Vector3>(){});
            foreach(Vector3 hex in fringes[k-1]){                    
                foreach(Vector3 neighbor in neighbors(hex, hexMap)){
                    if(!visited.Contains(neighbor)){ 
                        visited.Add(neighbor);
                        fringes[k].Add(neighbor);
                    }
                }
            }
        }
        return visited;
    }

    public List<Vector3> unitRange(GameObject unit, int range, Dictionary<Vector3, GameObject> hexMap){
       
        Vector3 start = rect2Cube(unit.transform.position);
        List<Vector3> visited = new List<Vector3>();
        List<List<Vector3>> fringes = new List<List<Vector3>>();
        visited.Add(start);
        fringes.Add(new List<Vector3>(){start});

        for (int k = 1; k <= range; k++){
            fringes.Add(new List<Vector3>(){});
            foreach(Vector3 hex in fringes[k-1]){                    
                foreach(Vector3 neighbor in neighbors(hex, hexMap)){
                    //check if neighbor contain a unit
                    //if true, check if units are enemies
                    //if true, skip neighbor, else continue
                    if(hexMap[neighbor].GetComponent<HexCell>().Unit != null){
                        if(areEnemies(unit, hexMap[neighbor].GetComponent<HexCell>().Unit)){                            
                            continue;
                        }
                    }
                    if(!visited.Contains(neighbor)){ 
                        visited.Add(neighbor);
                        fringes[k].Add(neighbor);
                    }
                }
            }
        }
        return visited;
    }

    public List<Vector3> attackRange(GameObject unit, int range, Dictionary<Vector3, GameObject> hexMap){
        Vector3 start = rect2Cube(unit.transform.position);
        List<Vector3> visited = new List<Vector3>();
        List<List<Vector3>> fringes = new List<List<Vector3>>();
        visited.Add(start);
        fringes.Add(new List<Vector3>(){start});

        for (int k = 1; k <= range; k++){
            fringes.Add(new List<Vector3>(){});
            foreach(Vector3 hex in fringes[k-1]){                    
                foreach(Vector3 neighbor in neighbors(hex, hexMap)){
                    //check if neighbor contain a blocked cell
                    //if true, skip neighbor, else continue
                    if(hexMap[neighbor].GetComponent<HexCell>().Blocked){
                            continue;
                    }
                    if(!visited.Contains(neighbor)){ 
                        visited.Add(neighbor);
                        fringes[k].Add(neighbor);
                    }
                }
            }
        }
        return visited;
    } 

    //============================================================

    
    
    public bool areEnemies(GameObject unitA, GameObject unitB){
        string factionA = unitA.GetComponent<UnitProps>().Faction;
        string factionB = unitB.GetComponent<UnitProps>().Faction;
        if(factionA == factionB){
            return false;
        }else
        {
            return true;
        }
    }
    public bool blockedByUnitType(GameObject hex, GameObject unit){
        if(hex.GetComponent<HexCell>().Unit != null){
            foreach(string bypass in unit.GetComponent<UnitProps>().BypassList){
                if(hex.GetComponent<HexCell>().Unit.GetComponent<UnitProps>().Type.Equals(bypass)){
                    return true;                        
                }
            }
        }          
        return false;
    }

    public bool hexIsPassable(GameObject hex, Dictionary<Vector3, GameObject> hexGrid){
        Vector3 hexCords = hex.GetComponent<HexCell>().Cordinates;

        if(hexGrid.ContainsKey(hexCords)){///and
            if(hexGrid[hexCords].GetComponent<HexCell>().Passable){
                return true;
            }                
        } 
        return false; 
        }

    public void passableCheck(List<Action> methodList){
        foreach(Action method in methodList){
            method();
        }
    }

    



}   

    // public List<Vector3> pathFind(Vector3 start, Vector3 goal, Dictionary<Vector3, GameObject> hexMap)
    //     {
    //         // Vector3 start = startCell.GetComponent<HexCell>().Cordinates;
    //         // Vector3 goal = goalCell.GetComponent<HexCell>().Cordinates;
    //         SimplePriorityQueue<Vector3> frontier = new SimplePriorityQueue<Vector3>();
    //         Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
    //         Dictionary<Vector3, int> costSoFar = new Dictionary<Vector3, int>();
    //         List<Vector3> path = new List<Vector3>();
    //         Vector3 current;
    //         //ReachableConditions reachableCondition = new ReachableConditions();
            
    //         frontier.Enqueue(start, 0);
    //         cameFrom.Add(start, default(Vector3));
    //         costSoFar.Add(start, 0);

    //         while(frontier.Count > 0){
    //             current  = frontier.Dequeue();
    //             if(current.Equals(goal)){
    //                 break;
    //             }                
    //             foreach(Vector3 next in neighbors(current, hexMap)){
    //                 int newCost = costSoFar[current] + hexMap[current].GetComponent<HexCell>().Cost;
    //                 if(!costSoFar.ContainsKey(next) || newCost<costSoFar[next]){
    //                     costSoFar[next] = newCost;
    //                     int priority = newCost + Heruistic(goal, next);
    //                     frontier.Enqueue(next, priority);
    //                     cameFrom[next] = current;
    //                 }
    //             }
    //         }
    //         current = goal;
    //         while(current != start){
    //             path.Add(current);
    //             current = cameFrom[current];
    //         } 
    //         path.Add(start);
    //         path.Reverse();
    //         return path;
    //     } 


    



