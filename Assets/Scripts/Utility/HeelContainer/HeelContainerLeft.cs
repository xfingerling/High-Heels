public class HeelContainerLeft : HeelContainer
{
    private void LateUpdate()
    {
        if (_player != null)
            transform.position = _player.LeftHeelContainer.position - _offset;
    }
}
