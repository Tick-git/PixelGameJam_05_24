public class SoundBuilder
{
    readonly SoundManager _soundManager;
    SoundData _soundData;
    bool _randomPitch;

    public SoundBuilder(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    public SoundBuilder WithSoundData(SoundData soundData)
    {
        _soundData = soundData;
        return this;
    }

    public SoundBuilder WithRandomPitch()
    {
        _randomPitch = true;
        return this;
    }

    public void Play()
    {
        if (!SoundManager.Instance.CanPlaySound(_soundData)) return;
        
        SoundEmitter soundEmitter = SoundManager.Instance.GetSoundEmitter();
        soundEmitter.Initialize(_soundData);
        soundEmitter.transform.parent = SoundManager.Instance.transform;

        if(_randomPitch)
        {
            soundEmitter.WithRandomPitch();
        }

        if (_soundManager.Counts.TryGetValue(_soundData, out int count))
        {
            _soundManager.Counts[_soundData] = count + 1;
        }
        else
        {
            _soundManager.Counts.Add(_soundData, 1);
        }

        soundEmitter.Play();
    }
}
