using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public enum eMusic { BARREN_BOSS, BARREN_COMBAT, BOSS, ENEMIES, EVADING, TENSE }

    private static MusicSwitcher INSTANCE;

    [SerializeField]
    private AudioClip barrenBossClip;

    [SerializeField]
    private AudioClip barrenCombatClip;

    [SerializeField]
    private AudioClip bossClip;

    [SerializeField]
    private AudioClip enemiesClip;

    [SerializeField]
    private AudioClip evadingClip;

    [SerializeField]
    private AudioClip tenseClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (INSTANCE)
            Destroy(gameObject);
        else
            INSTANCE = this;
    }

    public static void SwitchMusic(eMusic musicType)
    {
        switch (musicType)
        {
            case eMusic.BARREN_BOSS:
                INSTANCE.audioSource.clip = INSTANCE.barrenBossClip;
                break;
            case eMusic.BARREN_COMBAT:
                INSTANCE.audioSource.clip = INSTANCE.barrenCombatClip;
                break;
            case eMusic.BOSS:
                INSTANCE.audioSource.clip = INSTANCE.bossClip;
                break;
            case eMusic.ENEMIES:
                INSTANCE.audioSource.clip = INSTANCE.enemiesClip;
                break;
            case eMusic.EVADING:
                INSTANCE.audioSource.clip = INSTANCE.evadingClip;
                break;
            case eMusic.TENSE:
                INSTANCE.audioSource.clip = INSTANCE.tenseClip;
                break;
        }
    }
}
