#if UNITY_EDITOR
//#define DefineVoxelEditor
#endif

using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
public partial class VoxelEditor : MonoBehaviour
{
    //35x35chunk相关参数
    public VFVoxelTerrain m_voxelTerrain;
    const int m_shift = VoxelTerrainConstants._shift;
    const int m_voxelNumPerChunkMask = VoxelTerrainConstants._mask;
	const int m_SubTerCellSize = 32;

    public GameObject[] m_treePrototypeList; //树model列表
	public float[] m_treePrototypeBendfactor;
	public float[] m_treePrototypeSizes; //tree size
    public List<string> m_treePrototypeName;   //PrototypeList的文件名

    public static VoxelEditor m_this; //singleton实例
	
    //获取singleton对象
    public static VoxelEditor Get()
    {
        return m_this;
    }

	void Init()
	{
		m_voxelTerrain = VFVoxelTerrain.self;
		if(m_voxelTerrain != null)
		{
			gameObject.transform.parent = m_voxelTerrain.gameObject.transform;
		}
	}

    void Awake()
    {
        m_this = this;
		//ms_isSubTerrainOdtGen = false;

		if (GameConfig.IsMultiMode)
			enabled = false;
		else
			enabled = true;
    }
    // Use this for initialization
    void Start()
    { 
		Init ();
//        m_subTerrainGroup = GameObject.Find("SubTerrainGroup");
        m_treePrototypeName = new List<string>();
    }

#if DefineVoxelEditor
	void Update()
    {
        if (m_voxelTerrain != null) {
			if (m_eEditMode != EEditMode.eEditNormSpec) {
				UpdateBuildOp (); //更新build行为
			}
		}
		else
		{
			Init ();
		}
    }
#endif
	
    //读取以指定序号voxel为中心，半径 + 过滤区域 大小的范围内的所有voxel
    VFVoxel[, ,] ReadVoxels(IntVector3 _centerIdx, int _alterRadius, int _filterSize)
    {
        int _radius = _alterRadius + _filterSize;
        int _size = 2 * _radius + 1;
        VFVoxel[, ,] _voxels = new VFVoxel[_size, _size, _size];
        IVxDataSource _vds = m_voxelTerrain.Voxels;
        IntVector3 _chunkIdx = new IntVector3();

        for (int _ix = -_radius; _ix <= _radius; ++_ix)
        {
            for (int _iy = -_radius; _iy <= _radius; ++_iy)
            {
                for (int _iz = -_radius; _iz <= _radius; ++_iz)
                {
                    int _vx = _centerIdx.x + _ix;
                    int _vy = _centerIdx.y + _iy;
                    int _vz = _centerIdx.z + _iz;

                    if (_vx < 0 || _vy < 0 || _vz < 0)
                        continue;
                
                    _chunkIdx.x = _vx >> m_shift;
                    _chunkIdx.y = _vy >> m_shift;
                    _chunkIdx.z = _vz >> m_shift;
                
                    VFVoxelChunkData _vc = _vds.readChunk(_chunkIdx.x, _chunkIdx.y, _chunkIdx.z);
                    _vx = (_vx & m_voxelNumPerChunkMask);
                    _vy = (_vy & m_voxelNumPerChunkMask);
                    _vz = (_vz & m_voxelNumPerChunkMask);
                    _voxels[_ix + _radius, _iy + _radius, _iz + _radius] = _vc.ReadVoxelAtIdx(_vx, _vy, _vz);
                }
            }
        }

        return _voxels;
    }
}
