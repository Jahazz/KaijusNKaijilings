using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.UI;

public class ObjectToCanvasRaycaster : MonoBehaviour
{
    public delegate void OnBookPreparedParams ();
    public event OnBookPreparedParams OnBookPrepared;

    [field: SerializeField]
    private bool IsDebug { get; set; }

    [field: Space]
    [field: SerializeField]
    private List<PageCanvasRelation> PageCanvasInspectorCollection { get; set; } = new List<PageCanvasRelation>();
    [field: SerializeField]
    private Camera BookCamera { get; set; }

    private PageCanvasRelation LeftPage { get; set; }
    private PageCanvasRelation RightPage { get; set; }
    public static bool IsCursorOverrideEnabled { get; private set; }
    public static Vector2 CustomPagePosition { get; private set; }

    public void FlipPageLeftToRight (float playbackSpeed = 1.0f)
    {
        if (LeftPage != null && LeftPage?.CanvasControllerTopInstance != null)
        {
            LeftPage.PageControllerInstance.FlipPage(playbackSpeed);
        }

        RecalculatePages();
    }

    public void FlipPageRightToLeft (float playbackSpeed = 1.0f)
    {

        if (RightPage != null && RightPage?.CanvasControllerBottomInstance != null)
        {
            RightPage.PageControllerInstance.FlipPage(playbackSpeed);
        }

        RecalculatePages();
    }

    public void OpenPage (int targetPageIndex)
    {
        StartCoroutine(OpenPageCoroutine(targetPageIndex));
    }

    protected virtual void Update ()
    {
        RaycastPages();
    }

    private IEnumerator OpenPageCoroutine (int targetPageIndex)
    {
        PageCanvasRelation pageToGet = PageCanvasInspectorCollection[targetPageIndex];

        if (pageToGet != LeftPage)
        {
            int leftPageIndex = PageCanvasInspectorCollection.IndexOf(LeftPage);

            for (int i = 0; i < Mathf.Abs(leftPageIndex - targetPageIndex); i++)
            {
                if (leftPageIndex > targetPageIndex)
                {
                    yield return new WaitWhile(RightPage.PageControllerInstance.IsAnimating);
                    FlipPageLeftToRight(2);
                }
                else
                {
                    yield return new WaitWhile(LeftPage.PageControllerInstance.IsAnimating);
                    FlipPageRightToLeft(2);
                }
            }
        }
    }

    private void RaycastPages ()
    {
        Ray ray = BookCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            CanvasController currentCanvasController = null;

            if (hit.collider == LeftPage?.PageControllerInstance.TargetObjectCollider)
            {
                currentCanvasController = LeftPage.CanvasControllerBottomInstance;
            }
            else if (hit.collider == RightPage?.PageControllerInstance.TargetObjectCollider)
            {
                currentCanvasController = RightPage.CanvasControllerTopInstance;
            }

            if (currentCanvasController != null)
            {
                CheckHitForPage(ray, hit, currentCanvasController);
            }
            else
            {
                DisableAllExcept(null);
                IsCursorOverrideEnabled = false;
            }
        }
        else
        {
            DisableAllExcept(null);
            IsCursorOverrideEnabled = false;
        }
    }


    private void CheckHitForPage (Ray ray, RaycastHit hit, CanvasController currentCanvasController)
    {
        Vector2 textureCoord = hit.textureCoord;
        textureCoord = new Vector2(textureCoord.x * currentCanvasController.UiMainRectTransform.rect.width, textureCoord.y * currentCanvasController.UiMainRectTransform.rect.height);

        if (IsDebug == true)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        DisableAllExcept(currentCanvasController);

        IsCursorOverrideEnabled = true;
        CustomPagePosition = textureCoord;
    }

    public void DisableAllExcept (CanvasController currentCanvasController)
    {
        foreach (PageCanvasRelation item in PageCanvasInspectorCollection)
        {
            if (item.CanvasControllerBottomInstance != currentCanvasController)
            {
                item.CanvasControllerBottomInstance?.SetStateOfRaycaster(false);
            }
            else
            {
                item.CanvasControllerBottomInstance?.SetStateOfRaycaster(true);
            }

            if (item.CanvasControllerTopInstance != currentCanvasController)
            {
                item.CanvasControllerTopInstance?.SetStateOfRaycaster(false);
            }
            else
            {
                item.CanvasControllerTopInstance?.SetStateOfRaycaster(true);
            }
        }
    }


    public virtual void Awake ()
    {
        SetPagesToDefaultStates();
        RecalculatePages();
        StartCoroutine(NotifyPreparationCompleted());
    }

    public IEnumerator NotifyPreparationCompleted ()
    {
        yield return new WaitUntil(AreAllPagesTurned);
        OnBookPrepared?.Invoke();
    }

    private void SetPagesToDefaultStates ()
    {
        foreach (var page in PageCanvasInspectorCollection)
        {
            page.PageControllerInstance.SetPageToSide(page.PageControllerInstance.SideOfPage);
        }
    }

    private bool AreAllPagesTurned ()
    {
        bool areAllPagesTurned = true;

        foreach (var page in PageCanvasInspectorCollection)
        {
            if(page.PageControllerInstance.IsAnimating()== true)
            {
                areAllPagesTurned = false;
                break;
            }
        }

        return areAllPagesTurned;
    }

    private PageCanvasRelation GetPageCanvasPair (PageController controllerToFindBy)
    {
        return PageCanvasInspectorCollection.Where(x => x.PageControllerInstance == controllerToFindBy).FirstOrDefault();
    }
    private PageCanvasRelation GetPageCanvasPair (CanvasController controllerToFindBy)
    {
        return PageCanvasInspectorCollection.Where(x => x.CanvasControllerTopInstance == controllerToFindBy || x.CanvasControllerBottomInstance == controllerToFindBy).FirstOrDefault();
    }

    public void FitToObject ()
    {
        //Vector3 TargetColliderSize = TargetObjectCollider.bounds.size;
        //float ratio = TargetColliderSize.z / TargetColliderSize.x;
        //TargetRenderTexture.Release();
        //TargetRenderTexture.width = TargetRenderTextureWidth;
        //TargetRenderTexture.height = (int)(TargetRenderTexture.width * ratio);
        //TargetRenderTexture.Create();
    }

    private void OnDisable ()
    {
        IsCursorOverrideEnabled = false;
    }

    private void RecalculatePages ()
    {
        PageCanvasRelation leftPage = null;
        PageCanvasRelation rightPage = null;


        PageCanvasRelation page;

        for (int i = PageCanvasInspectorCollection.Count - 1; i >= 0; i--)
        {
            page = PageCanvasInspectorCollection[i];

            if (leftPage == null && page.PageControllerInstance.SideOfPage == PageSide.LEFT)
            {
                leftPage = page;
            }
        }

        for (int i = 0; i < PageCanvasInspectorCollection.Count; i++)
        {
            page = PageCanvasInspectorCollection[i];

            if (rightPage == null && page.PageControllerInstance.SideOfPage == PageSide.RIGHT)
            {
                rightPage = page;
            }
        }

        LeftPage = leftPage;
        RightPage = rightPage;

        int leftIndex = PageCanvasInspectorCollection.IndexOf(leftPage);
        int rightIndex = PageCanvasInspectorCollection.IndexOf(rightPage);

        for (int i = leftIndex; i >= 0; i--)//todo: just one loop with if
        {
            PageCanvasInspectorCollection[i].PageControllerInstance.SetTopPageMark(i - 1);
        }

        for (int i = rightIndex; i < PageCanvasInspectorCollection.Count; i++)
        {
            PageCanvasInspectorCollection[i].PageControllerInstance.SetTopPageMark(rightIndex - i - 1);
        }

        LeftPage?.PageControllerInstance.SetTopPageMark(leftIndex);
        RightPage?.PageControllerInstance.SetTopPageMark(rightIndex);

        DisableUnusedPagesCameras();
    }

    private void DisableUnusedPagesCameras ()
    {
        foreach (PageCanvasRelation pageRelation in PageCanvasInspectorCollection)
        {
            pageRelation?.CanvasControllerBottomInstance?.SetCameraActive(false);
            pageRelation?.CanvasControllerTopInstance?.SetCameraActive(false);

            if (pageRelation.CanvasControllerBottomInstance != null && pageRelation == LeftPage)
            {
                pageRelation.CanvasControllerBottomInstance.SetCameraActive(true);
            }
            else if (pageRelation.CanvasControllerTopInstance != null && pageRelation == RightPage)
            {
                pageRelation.CanvasControllerTopInstance.SetCameraActive(true);
            }
        }
    }
}
