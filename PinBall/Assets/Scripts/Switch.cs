using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Collider bola;

    // menyimpan variabel material nyala dan mati untuk merubah warna
    public Material offMaterial;
    public Material onMaterial;

    // menyimpan state switch apakah nyala atau mati
    bool isOn;

    // komponen renderer pada object yang akan diubah
    private Renderer render;

    // enum untuk mengatur state
    private enum SwitchState
    {
        Off,
        On,
        Blink
    }
    
    // menggantikan isOn
    private SwitchState state;

    void Start()
    {
        bola = GameObject.Find("Ball").GetComponent<Collider>();
        render = GetComponent<Renderer>();

        Set(false);

        // pada fungsi start mulai langsung jalankan timer
        StartCoroutine(BlinkTimerStart(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Set(bool active)
    {
        if (active == true)
        {
            state = SwitchState.On;
            render.material = onMaterial;

            // hentikan proses blink
            StopAllCoroutines();
        }
        else
        {
            state = SwitchState.Off;
            render.material = offMaterial;

            // saat dimatikan, lagsung mulai timer nya
	        StartCoroutine(BlinkTimerStart(5));
        }
        // if(isOn == active)
        // {
        //     render.material = onMaterial;
        // }
        // else
        // {
        //     render.material = offMaterial;
        // }
    }

    void OnTriggerEnter(Collider collider)
    {
        if ( collider == bola)
        {
            Debug.Log("Kena Switch");

            // kita matikan atau nyalakan switch sesuai dengan kebalikan state switch tersebut
		    // mati --> nyala || nyala --> mati
            //Set(!isOn);
            
            //StartCoroutine(Blink(2));

            Toggle();
        }
    }
    private void Toggle()
    {
        // dari on --> off
        if (state == SwitchState.On)
        {
            Set(false);
        }
        // dari off --> on atau blink --> on
        else
        {
            Set(true);
        }
    }

    private IEnumerator Blink(int times)
    {
        // set statenya menjadi blink dulu sebelum mulai proses
        state = SwitchState.Blink;

        // ulang perubahan nyala mati sebanyak parameter
        for(int i = 0; i < times; i++)
        {
            render.material = onMaterial;
            yield return new WaitForSeconds(0.5f);
            render.material = offMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        
        // set menjadi off kembali setelah proses blink
        state = SwitchState.Off;

        // saat selesai blink, mulai juga timer nya
        StartCoroutine(BlinkTimerStart(5));
    }

    private IEnumerator BlinkTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Blink(2));
    }
}
