using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DamagePlayer))]
public class DamagePlayerEditor : Editor
{
    private void OnSceneGUI() {
        DamagePlayer damagePlayer = (DamagePlayer)target;
        Handles.color = Color.red;

        Handles.DrawWireArc(damagePlayer.transform.position, Vector3.up, Vector3.forward, 360, damagePlayer.attackRange);
    }
}
