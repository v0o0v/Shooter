using UnityEngine;
using System.Collections;

/// <summary>
/// BGScroller script.
/// Scroll Background textures and Clouds.
/// </summary>
public class BGScroller : MonoBehaviour
{
    public SpriteRenderer[] bgs;
    public Sprite[] bgSprites;
    [RangeAttribute(5f, 15f)]
    public float bgScrollSpeed = 8f;
    private int _indexToSwitch = 0;

    public SpriteRenderer[] clouds;
    public Sprite[] cloudSprites;
    [RangeAttribute(5f, 10f)]
    public float cloudScrollSpeedMin = 9f;
    [RangeAttribute(10f, 15f)]
    public float cloudScrollSpeedMax = 10f;
    [RangeAttribute(0.5f, 1f)]
    public float cloudSizeMin = 1f;
    [RangeAttribute(3f, 7f)]
    public float cloudSizeMax = 5f;
    public float screenWidthHalf;
    public float screenHeightHalf;

    private Vector2[] _cloudOriginalSize;
    private float[] _cloudSpeed;

    // Initialization.
    void Start()
    {
        _cloudOriginalSize = new Vector2[clouds.Length];
        _cloudSpeed = new float[clouds.Length];

        for (int ix = 0; ix < clouds.Length; ++ix)
        {
            _cloudOriginalSize[ix] = new Vector2(clouds[ix].transform.localScale.x, clouds[ix].transform.localScale.y);
            CloudReset(ix);
        }
    }

    void Update()
    {
        // swap background srptie and change position.
        if (bgs[_indexToSwitch].transform.position.y <= -screenHeightHalf)
        {
            // change spriterender's position and change sprite randomly.
            bgs[_indexToSwitch].transform.position = new Vector3(0f, screenHeightHalf, 0f);
            bgs[_indexToSwitch].sprite = bgSprites[Random.Range(0, bgSprites.Length)];

            _indexToSwitch = _indexToSwitch == 0 ? 1 : 0;
        }

        // Scroll Background.
        for (int ix = 0; ix < bgs.Length; ++ix)
        {
            bgs[ix].transform.Translate(new Vector3(0f, -bgScrollSpeed, 0f) * Time.deltaTime);
        }

        // Scroll Cloud.
        for (int ix  =0; ix < clouds.Length; ++ix)
        {
            clouds[ix].transform.Translate(new Vector3(0f, -_cloudSpeed[ix], 0f) * Time.deltaTime);

            if (clouds[ix].transform.position.y <= -screenHeightHalf)
            {
                CloudReset(ix);
            }
        }
    }

    // Set Cloud's position and sprite.
    void CloudReset(int index)
    {
        clouds[index].transform.localPosition
            = new Vector3(Random.Range(-screenWidthHalf, screenWidthHalf), Random.Range(screenHeightHalf, screenHeightHalf * 2f));

        float randomSize = Random.Range(cloudSizeMin, cloudSizeMax);
        clouds[index].transform.localScale
            = new Vector3(_cloudOriginalSize[index].x * randomSize, _cloudOriginalSize[index].y * randomSize, 1f);

        _cloudSpeed[index] = Random.Range(cloudScrollSpeedMin, cloudScrollSpeedMax);
        clouds[index].sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
    }
}