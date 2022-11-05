public class HeelConteinerRight : HeelContainer
{
    private void LateUpdate()
    {
        if (_player != null)
            transform.position = _player.RightHeelContainer.position - _offset;
    }
}
