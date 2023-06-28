using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGimic : MonoBehaviour
{
    [SerializeField] Transform destination;

    private Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = PlayerManager.instance.player;
        rb = player.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PlayerFade());
            }
        }
    }

    private IEnumerator PlayerFade()
    {
        rb.simulated = false;

        player.fx.fadeOut = true;
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.7f);
        player.transform.position = destination.transform.position;
        player.SetZeroVelocity();
        player.fx.fadeIn = true;
        yield return new WaitForSeconds(0.7f);

        rb.simulated = true;

    }

    private IEnumerator MoveInPortal()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
