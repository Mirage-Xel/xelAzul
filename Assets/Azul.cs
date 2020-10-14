using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;
using KModkit;
public class Azul: MonoBehaviour {
    public KMSelectable[] selectables;
    public MeshRenderer[] tiles;
    public Material[] colors;
    MeshRenderer[][] tileArrays = new MeshRenderer[5][];
    bool[][] binary = new bool[][]{
         new bool[] {false, false, false, false, false },
         new bool[] {false, false, false, false, true },
         new bool[] {false, false, false, true, false },
         new bool[] {false, false, false, true, true },
         new bool[] {false, false, true, false, false },
         new bool[] {false, false, true, false, true },
         new bool[] {false, false, true, true, false },
         new bool[] {false, false, true, true, true },
         new bool[] {false, true, false, false, false },
         new bool[] {false, true, false, false, true },
         new bool[] {false, true, false, true, false },
         new bool[] {false, true, false, true, true },
         new bool[] {false, true, true, false, false },
         new bool[] {false, true, true, false, true },
         new bool[] {false, true, true, true, false },
         new bool[] {false, true, true, true, true },
         new bool[] {true, false, false, false, false },
         new bool[] {true, false, false, false, true },
         new bool[] {true, false, false, true, false },
         new bool[] {true, false, false, true, true },
         new bool[] {true, false, true, false, false },
         new bool[] {true, false, true, false, true },
         new bool[] {true, false, true, true, false },
         new bool[] {true, false, true, true, true },
         new bool[] {true, true, false, false, false },
         new bool[] {true, true, false, false, true },
         new bool[] {true, true, false, true, false, },
    };
    bool[][] board = new bool[5][];
    int[][] layout = new int[][] {
        new int[] {0, 1, 2, 3, 4},
        new int[] {4, 0, 1, 2, 3},
        new int[] {3, 4, 0, 1, 2}, 
        new int[] {2, 3, 4, 0, 1},
        new int[] {1, 2, 3, 4, 0}
    };
    int score = 1;
    List<int> storedRows = new List<int>();
    char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    public KMBombModule module;
    public KMBombInfo bomb;
    public KMAudio sound;
    int moduleId;
    static int moduleIdCounter = 1;
    bool solved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        List<MeshRenderer>[] tileArraysTemp = new List<MeshRenderer>[] { new List<MeshRenderer>(), new List<MeshRenderer>(), new List<MeshRenderer>(), new List<MeshRenderer>(), new List<MeshRenderer>() };
        int k = 0;
        int l = 0;
        for (int i = 0; i < 5; i++)
        {
            k = l;
            for (int j = 0; j <= i; j++)
            {
                l++;
                tileArraysTemp[i].Add(tiles[k + j]);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            tileArrays[i] = tileArraysTemp[i].ToArray();
        }
        foreach(KMSelectable i in selectables)
        {
            KMSelectable j = i;
            j.OnInteract += delegate { submitScore(i, Array.IndexOf(selectables, j)); return false; };
        }
    }

    void Start () {
        foreach (MeshRenderer i in tiles)
        {
            i.enabled = true;
        }
		for (int i = 0; i < 5; i++)
        {
            if (Array.IndexOf(alphabet, bomb.GetSerialNumber()[i]) != -1)
            {
                board[i] = binary[Array.IndexOf(alphabet, bomb.GetSerialNumber()[i]) + 1];
            }
            else
            {
                board[i] = binary[int.Parse(bomb.GetSerialNumber()[i].ToString())];
            }
        }
        for (int i = 0; i < 2; i++)
        {
            int j;
            int k;
            do
            {
                 j = rnd.Range(1, 5);
                 k = rnd.Range(0, 5);
            } while (!board[j][k] || storedRows.Contains(j));
            board[j][k] = false;
            for (int l = rnd.Range(1, tileArrays[j].Length); l < tileArrays[j].Length; l++ )
            {
                tileArrays[j][l].enabled = false;
            }
            for (int l = 0; l < tileArrays[j].Length; l++)
            {
                if (tileArrays[j][l].enabled)
                {
                    tileArrays[j][l].material = colors[layout[j][k]];
                }
            }
            storedRows.Add(j);
        }
        foreach (MeshRenderer[] i in tileArrays)
        {
            if (!storedRows.Contains(Array.IndexOf(tileArrays, i)))
            {

                foreach (MeshRenderer j in i)
                {
                    j.enabled = false;
                }
            }
        }
    }

    void submitScore(KMSelectable selectable, int index) {
		
	}
}
