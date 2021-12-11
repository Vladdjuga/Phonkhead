using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonController : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{
    public Character character;
    public float horizontal;
    public bool is_jumped;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (is_jumped&& (!character.is_jumping || character.jump_count < 2))
        {
            character.jump_count++;
            if (character.jump_count == 2)
            {
                character.anim.SetBool("is_double_jump", true);
            }
            character.is_jumping = true;
            character.anim.SetBool("is_jumping", true);
            character.jump(character.jump_v);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        character.move=horizontal;
        //Debug.LogError("aoodoas");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        character.move = 0;
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
