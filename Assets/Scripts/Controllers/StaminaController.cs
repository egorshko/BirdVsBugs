﻿using UnityEngine;

public class StaminaController : MonoBehaviour
{
	private StaminaSlider _staminaSlider;
	private EnemyEffects _enemyEffects;
	private UIController _uiController;
	private WinPanelModel _winPanel;
	private EatingModel _eatingModel;
	private RotatingObject _rotatingObject;
	private Coins _coinsModel;
	private Boss _bossModel;
	private bool _gameEnded;
	private void Awake()
	{
		_staminaSlider = FindObjectOfType<StaminaSlider>();
		_enemyEffects = FindObjectOfType<EnemyEffects>();
		_uiController = FindObjectOfType<UIController>();
		_winPanel = FindObjectOfType<WinPanelModel>();
		_rotatingObject = FindObjectOfType<RotatingObject>();
		_coinsModel = FindObjectOfType<Coins>();
		_eatingModel = FindObjectOfType<EatingModel>();
		_bossModel = FindObjectOfType<Boss>();
		_staminaSlider.BasicStaminaOffMultiplyer = _staminaSlider.StaminaOffMultiplyer;
	}
	private void FixedUpdate()
	{
		_staminaSlider.ReduceStaminaByTime();
		if (_staminaSlider.BirdPower <= 0 && !_gameEnded)
		{
			if (_bossModel.IsBossFightNow)
			{
				_eatingModel.СanBiteAtAll = false;
				for (int i = 1; i <= 8; i++)
				{
					Invoke("SlowDownTimeScale", 0.01f * i * (1 - 0.1f * i));
				}
				Invoke("EndGame", 0.3f);
				_gameEnded = true;
			}
			else
			{
				_uiController.LoseGame();
				_gameEnded = true;
			}
		}
	}
	private void SlowDownTimeScale()
	{
		Time.timeScale -= 0.1f;
	}
	//TODO вынести в другой класс
	public void EndGame()
	{
		_rotatingObject.NeedToRotate = false;
		_uiController.WinGame();
		_winPanel.ActivateWinPanel(_bossModel.BossNumber);
		_rotatingObject.RotatingObjectTransform.gameObject.SetActive(false);
		Time.timeScale = 1;
		_coinsModel.AddCoinsCollectedOnLvl();
	}
	public void MakeStaminaGoOffFaster()
	{
		_staminaSlider.StaminaOffMultiplyer = _enemyEffects.MultiplyerOfPoisoned;
		Invoke("MakeStaminaGoOffSlower", _enemyEffects.TimeOfPoisoned);
	}
	private void MakeStaminaGoOffSlower()
	{
		_staminaSlider.StaminaOffMultiplyer = _staminaSlider.BasicStaminaOffMultiplyer;
	}
}
