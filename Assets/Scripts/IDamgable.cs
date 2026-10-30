using UnityEngine;

public interface IDamgable
{
    void onDamage(float dagmage, Vector3 hitPosition, Vector3 hitNormal);
}
