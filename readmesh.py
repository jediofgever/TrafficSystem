import open3d as o3d

road_mesh = o3d.io.read_triangle_mesh("/home/atas/ExportObj.obj")
sidewalk_mesh = o3d.io.read_triangle_mesh("/home/atas/ExportObj.obj")
road_mesh.paint_uniform_color([1, 0.706, 0])
sidewalk_mesh.paint_uniform_color([0, 0.706, 0])

# visualize the meshes with different colors
o3d.visualization.draw_geometries([road_mesh, sidewalk_mesh])

road_pcd = road_mesh.sample_points_uniformly(number_of_points=4*len(road_mesh.vertices))
sidewalk_pcd = sidewalk_mesh.sample_points_uniformly(number_of_points=4*len(sidewalk_mesh.vertices))

road_pcd.paint_uniform_color([1, 0.706, 0])
sidewalk_pcd.paint_uniform_color([0, 0.706, 0])
o3d.visualization.draw_geometries([road_pcd, sidewalk_pcd])

print(road_mesh.get_min_bound())
print(road_mesh.get_max_bound())

o3d.io.write_point_cloud("/home/atas/copy_of_fragment.pcd", pcd)
