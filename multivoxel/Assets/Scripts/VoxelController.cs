﻿using UnityEngine;

// Represents the voxel model in the editor
[RequireComponent(typeof(VoxelModelRenderer))]
public class VoxelController : MonoBehaviour
{
    private const float VoxelTransitionTime = 5f;

	public static VoxelModelRenderer _voxelRenderer;
    private VoxelAnimation _voxelAnimation;
	private VoxelData _voxelData;

	private void Awake() 
	{
		_voxelRenderer = GetComponent<VoxelModelRenderer>();
	    _voxelAnimation = GetComponent<VoxelAnimation>();
		_voxelData = new VoxelData();
		transform.position = new Vector3(-.5f, .5f, -.5f);
	}

	private void Start() 
	{
        _voxelRenderer.RenderMesh(_voxelData);
	}

    public void ChangeData(VoxelData voxelData)
    {
        _voxelData = voxelData;
        _voxelRenderer.RenderMesh(_voxelData);
    }

    public void AddVoxel(Voxel voxel)
    {

        _voxelAnimation.AddVoxelAnimation(_voxelData.HasVoxelAtPos(voxel.Pos), voxel, () =>
        {
            _voxelData.AddVoxel(voxel);
            _voxelRenderer.RenderMesh(_voxelData);
        });
    }

    public void RemoveVoxel(Vector3Int pos)
    {
        if (_voxelData.HasVoxelAtPos(pos))
        {
            Voxel voxel = new Voxel();
            voxel = _voxelData.VoxelAtPos(pos);
            _voxelData.RemoveVoxel(pos);
            _voxelRenderer.RenderMesh(_voxelData);
            _voxelAnimation.RemoveVoxelAnimation(true, voxel, () =>
            {
                // None needed
            });
        }
    }

    /// <summary>
    /// Only load from file before the scene is live (before the user can edit the model)
    /// </summary>
    public void LoadFromFile(string filepath)
    {
        _voxelData = VoxelSerializer.VoxelDataFromFile(filepath);
        _voxelRenderer.RenderMesh(_voxelData);
    }

    public void SaveToFile(string filepath)
    {
        if (filepath.EndsWith(".obj", System.StringComparison.InvariantCultureIgnoreCase))
            VoxelSerializer.VoxelMeshToObjFile(filepath, _voxelRenderer.MeshFilter);
        else
            VoxelSerializer.VoxelDataToFile(filepath, _voxelData);
    }
}
