/* Name: Kyle Dunn
 * Date: Dec 3, 2019
 * Purpose: Manages slots around the player for the enemies to target.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public Transform[] slots;

    [SerializeField]
    int maxSlotsFilled; // Won't fill slots if the amount filled is at this value

    [SerializeField]
    GameObject[] slotFilled;

    int slotCount;

    public bool Filled { get { return slotCount == maxSlotsFilled; } }

    /// <summary>
    /// Grabs the given slot's position with the slot index
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <returns>The given slot's position</returns>
    public Vector3 GetSlotPosition(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
            return transform.position;
        else
            return slots[slotIndex].position;
    }

    /// <summary>
    /// Finds the closest slot to the enemy and assigns it to that enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns>The filled slot's index. If -1 then couldn't fill slot.</returns>
    public int FillSlot(GameObject enemy)
    {
        if (slotCount >= slots.Length || slotCount >= maxSlotsFilled) return -1; // Doesn't add any more to the slots when over the number of slots or the max that can be filled

        // Finds the closest slot
        int closestSlot = -1;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slotFilled[i] != null) continue;

            float distance = Vector3.Distance(slots[i].position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestSlot = i;
                closestDistance = distance;
            }
        }

        if (closestSlot == -1)
            return -1;

        // Assigns slot to enemy
        slotFilled[closestSlot] = enemy;
        slotCount++;

        return closestSlot;
    }

    /// <summary>
    /// Releases the slot given
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <returns>Success in releasing slot</returns>
    public bool ReleaseSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length || slotCount == 0) return false;

        slotFilled[slotIndex] = null;
        slotCount--;

        return true;
    }

    private void Awake()
    {
        slotFilled = new GameObject[slots.Length];
    }

    /// <summary>
    /// Draws gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slotFilled[i])
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(slots[i].position, 1.5f);
        }
    }
}
