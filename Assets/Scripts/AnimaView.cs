using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _headRenderer;
    [SerializeField] private SpriteRenderer _bodyRenderer;

    private void Awake()
    {
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    private void OnStartSummon(OnStartSummon data)
    {

    }

    private void OnFinishSummon(OnFinishSummon data)
    {

    }

    private void Animate()
    {

    }
}
