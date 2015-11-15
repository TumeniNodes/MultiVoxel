﻿using UnityEngine;
using System.Collections.Generic;

// Holds voxel data
public class VoxelData 
{
	public IEnumerable<Voxel> Voxels 
	{
		get { return _data.Values; }
	}

	private readonly Dictionary<Vector3, Voxel> _data;

    public VoxelData() 
    {
    	_data = new Dictionary<Vector3, Voxel>();
    }

    public VoxelData(Voxel[] saveData) : base()
    {
        foreach (Voxel voxel in saveData)
        {
            _data[voxel.Pos] = voxel;
        }
    }

    // Will override any previous voxel that exists at the given pos
    public void AddVoxel(Vector3 pos, Color color) 
    {
    	_data[Utils.RoundVector3(pos)] = new Voxel(Utils.RoundVector3(pos), color);
    }

    public bool HasVoxelAtPos(Vector3 pos) 
    {
    	return _data.ContainsKey(Utils.RoundVector3(pos));
    }

    public Voxel[] GetSaveData()
    {
        Voxel[] saveData = new Voxel[_data.Count];
        _data.Values.CopyTo(saveData, 0);
        return saveData;
    }
}