using UnityEngine;
using UnityEditor;

public class CreateParentAtCenter
{
    [MenuItem("GameObject/Create Parent At Center", false, 0)]
    private static void CreateParent()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            // Начинаем регистрацию изменений для Undo
            Undo.SetCurrentGroupName("Create Parent At Center");
            int group = Undo.GetCurrentGroup();

            // Создаем новый пустой объект с тем же именем, что и выбранный объект
            GameObject parentObject = new GameObject("Parent:" + selectedObject.name);

            // Вычисляем центр меша выбранного объекта
            MeshFilter meshFilter = selectedObject.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                Vector3[] vertices = meshFilter.sharedMesh.vertices;
                Vector3 center = Vector3.zero;

                foreach (Vector3 vertex in vertices)
                {
                    center += selectedObject.transform.TransformPoint(vertex);
                }
                center /= vertices.Length;

                parentObject.transform.position = center;
            }
            else
            {
                parentObject.transform.position = selectedObject.transform.position;
            }

            // Устанавливаем родителем выбранный объект
            Transform previousParent = selectedObject.transform.parent;
            Undo.SetTransformParent(selectedObject.transform, parentObject.transform, "Set Parent");

            // Устанавливаем новый родительский объект дочерним к предыдущему родителю
            if (previousParent != null)
            {
                Undo.SetTransformParent(parentObject.transform, previousParent, "Set Parent of ParentObject");
            }

            Undo.CollapseUndoOperations(group);
        }
        else
        {
            Debug.LogWarning("Выберите объект для создания родителя.");
        }
    }
}
