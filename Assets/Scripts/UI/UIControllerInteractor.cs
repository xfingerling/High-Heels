using System.Collections.Generic;
using UnityEngine;

public class UIControllerInteractor : Interactor
{
    private UIInterface _UIInterface;
    private List<View> _popupViews = new List<View>();
    private View _gameHUD;

    public override void Initialize()
    {
        base.Initialize();

        GameObject uiInterfaceprefab = Resources.Load<GameObject>("UI/[INTERFACE]");
        GameObject goUIInterface = Object.Instantiate(uiInterfaceprefab);
        _UIInterface = goUIInterface.GetComponent<UIInterface>();

        InitPopupViews();
        InitGameHUD();
    }

    public T GetView<T>() where T : View
    {
        for (int i = 0; i < _popupViews.Count; i++)
        {
            if (_popupViews[i] is T tView)
                return tView;
        }

        return null;
    }

    public void ShowPopup<T>() where T : View
    {
        for (int i = 0; i < _popupViews.Count; i++)
        {
            if (_popupViews[i] is T)
            {
                _popupViews[i].transform.SetAsLastSibling();
                _popupViews[i].Show();
            }
        }
    }

    public void HideAllPopups()
    {
        foreach (var view in _popupViews)
            view.Hide();
    }

    public void ShowHUD() => _gameHUD.Show();

    public void HideHUD() => _gameHUD.Hide();


    private void InitPopupViews()
    {
        View[] viewPrefabs = Resources.LoadAll<View>("UI/Popups");
        Transform uiLayerPopupContainer = _UIInterface.PopupLayer.transform;

        foreach (var item in viewPrefabs)
        {
            var go = Object.Instantiate(item, uiLayerPopupContainer);
            _popupViews.Add(go);

            go.Initialize();
            go.Hide();
        }
    }

    private void InitGameHUD()
    {
        View HUDPrefab = Resources.Load<View>("UI/HUDGame");
        Transform uiLayerHUDContainer = _UIInterface.HudLayer.transform;

        _gameHUD = Object.Instantiate(HUDPrefab, uiLayerHUDContainer);
        _gameHUD.Initialize();
        _gameHUD.Hide();
    }
}
