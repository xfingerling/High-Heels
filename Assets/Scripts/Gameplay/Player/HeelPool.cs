using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class HeelPool
{
    private List<GameObject> _leftHeelsPool;
    private List<GameObject> _rightHeelsPool;
    private Transform _leftHeelContainer;
    private Transform _rightHeelContainer;

    public HeelPool(Transform container, int amount)
    {
        _leftHeelsPool = new List<GameObject>();
        _rightHeelsPool = new List<GameObject>();

        _leftHeelContainer = new GameObject("LeftHeelContainer").transform;
        _rightHeelContainer = new GameObject("RightHeelContainer").transform;

        _leftHeelContainer.SetParent(container);
        _rightHeelContainer.SetParent(container);

        _leftHeelContainer.AddComponent<HeelContainerLeft>();
        _rightHeelContainer.AddComponent<HeelConteinerRight>();

        FillPoolHeels(amount);
    }

    private void FillPoolHeels(int heelAmount)
    {
        GameObject heelPrefab = Resources.Load<GameObject>("Heel");

        for (int i = 0; i < heelAmount; i++)
        {
            GameObject goLeftHeel = Object.Instantiate(heelPrefab, _leftHeelContainer);
            GameObject goRightHeel = Object.Instantiate(heelPrefab, _rightHeelContainer);
            goLeftHeel.SetActive(false);
            goRightHeel.SetActive(false);

            _leftHeelsPool.Add(goLeftHeel);
            _rightHeelsPool.Add(goRightHeel);

            float goHeelScale = goLeftHeel.transform.localScale.y;
            Vector3 heelPosition = new Vector3(0, (goHeelScale + _leftHeelsPool.Count - 1) * -1, 0);

            goLeftHeel.transform.localPosition = heelPosition;
            goRightHeel.transform.localPosition = heelPosition;
        }
    }

    public int GetAmountActiveHeels()
    {
        return _leftHeelsPool.FindAll(heel => heel.activeInHierarchy).Count;
    }

    public void IncrementHeels()
    {
        GameObject leftHeel = _leftHeelsPool.Find(heel => !heel.activeInHierarchy);
        GameObject rightHeel = _rightHeelsPool.Find(heel => !heel.activeInHierarchy);
        leftHeel.SetActive(true);
        rightHeel.SetActive(true);
    }

    public void DicrementHeels(int value)
    {
        int heelCount = GetAmountActiveHeels();

        var leftActive = _leftHeelsPool.FindAll(heel => heel.activeInHierarchy);
        var rightActive = _rightHeelsPool.FindAll(heel => heel.activeInHierarchy);

        for (int i = leftActive.Count; i > leftActive.Count - value; i--)
        {
            int index = i - 1;

            leftActive[index].SetActive(false);
            rightActive[index].SetActive(false);
        }
    }
}
