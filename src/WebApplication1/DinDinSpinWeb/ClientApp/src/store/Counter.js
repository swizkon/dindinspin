const incrementCountType = 'INCREMENT_COUNT';
const decrementCountType = 'DECREMENT_COUNT';
const resetCountType = 'RESET_COUNT';
const initialState = { count: 0 };

interface IncrementSignalRCountAction { type: 'SIGNALR_INCREMENT_COUNT' }
interface DecrementSignalRCountAction { type: 'SIGNALR_DECREMENT_COUNT' }
interface ResetSignalRCountAction { type: 'SIGNALR_RESET_COUNT' }

type KnownAction = IncrementCountAction | DecrementCountAction | IncrementSignalRCountAction | DecrementSignalRCountAction | ResetSignalRCountAction;


export const actionCreators = {
    increment: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SIGNALR_INCREMENT_COUNT' });
        dispatch({ type: 'INCREMENT_COUNT' });
    },
    decrement: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SIGNALR_DECREMENT_COUNT' });
        dispatch({ type: 'DECREMENT_COUNT' });
    },
    reset: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SIGNALR_RESET_COUNT' });
        dispatch({ type: 'RESET_COUNT' });
    }
}

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === incrementCountType) {
    return { ...state, count: state.count + 1 };
  }

  if (action.type === resetCountType) {
      return { ...state, count: 0 };
  }

  if (action.type === decrementCountType) {
    return { ...state, count: state.count - 1 };
  }

  return state;
};
